using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using uPLibrary.Networking.M2Mqtt;
using VCardsMiddleware.Models;

namespace VCardsMiddleware.Controllers
{
    public class TransactionsController : ApiController
    {
        string connectionString = Properties.Settings.Default.DBConnString;

        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> GetTansactions(string type = null, int externalEntityId = -1, string dateFrom = null, string dateTo = null)
        {
            List<Transaction> transactions = new List<Transaction>();
            List<Transaction> fullTransactions = new List<Transaction>();
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                string sql = "SELECT endpoint FROM ExternalEntities";

                SqlCommand command = new SqlCommand(null, connection);

                if (externalEntityId >= 0)
                {
                    sql += " WHERE id = @id";
                    command.Parameters.AddWithValue("@id", externalEntityId);
                }
                connection.Open();

                command.CommandText = sql;

                SqlDataReader reader = command.ExecuteReader();

                string endpoint;

                HttpClient client = new HttpClient();

                string endpointIncrement = "";
                if (!string.IsNullOrEmpty(type) || !string.IsNullOrEmpty(dateFrom) || !string.IsNullOrEmpty(dateTo) || externalEntityId >= 0)
                {
                    endpointIncrement += '?';
                }

                if (!string.IsNullOrEmpty(type))
                {
                    endpointIncrement += "type=" + type;
                }

                if (!string.IsNullOrEmpty(dateFrom) && !string.IsNullOrEmpty(dateTo))
                {
                    if (!string.IsNullOrEmpty(type))
                    {
                        endpointIncrement += "&dateFrom=" + dateFrom + "&dateTo=" + dateTo;
                    }
                    else
                    {
                        endpointIncrement += "dateFrom=" + dateFrom + "&dateTo=" + dateTo;
                    }
                }

                if (externalEntityId >= 0)
                {
                    if (!string.IsNullOrEmpty(type) || (!string.IsNullOrEmpty(dateFrom) && !string.IsNullOrEmpty(dateTo)))
                    {
                        endpointIncrement += "&externalEntityId=" + externalEntityId;
                    }
                    else
                    {
                        endpointIncrement += "externalEntityId=" + externalEntityId;
                    }

                }

                while (reader.Read())
                {
                    endpoint = (string)reader["endpoint"];
                    endpoint += "api/transactions" + endpointIncrement;


                    HttpResponseMessage response = await client.GetAsync(endpoint);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        transactions = JsonConvert.DeserializeObject<List<Transaction>>(responseBody);
                    }
                    fullTransactions.AddRange(transactions);
                }

                return Ok(fullTransactions);

            }
            catch (Exception)
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                return InternalServerError();
            }
        }

        public async Task<IHttpActionResult> PostTansaction([FromBody] TransactionPost transaction)
        {
            if (transaction == null)
                return BadRequest();

            if (transaction.Payment_reference == null)
            {
                return Content((HttpStatusCode)422, "Invalid payment_reference");
            }

            if (transaction.Value <= 0)
                return Content((HttpStatusCode)422, "Invalid transaction value");

            if (transaction.Type != 'D' && transaction.Type != 'C')
                return Content((HttpStatusCode)422, "Invalid type of transaction");

            if (transaction.Description != null && transaction.Description.Length > 255)
                return Content((HttpStatusCode)422, "Invalid description (Must be smaller than 255 characters");

            if (transaction.Category_id < 0)
            {
                return Content((HttpStatusCode)422, "Invalid category");
            }

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);

                SqlCommand commandSearchVCard1 = new SqlCommand("SELECT * FROM VCards WHERE phone_number=@phone_number", connection);
                commandSearchVCard1.Parameters.AddWithValue("@phone_number", transaction.VCard);
                SqlCommand commandSearchVCard2 = new SqlCommand("SELECT * FROM VCards WHERE phone_number=@phone_number", connection);
                commandSearchVCard2.Parameters.AddWithValue("@phone_number", transaction.Payment_reference);

                connection.Open();

                SqlDataReader readerCard1 = commandSearchVCard1.ExecuteReader();

                int externalEntitySource;

                if (!readerCard1.Read())
                {
                    readerCard1.Close();
                    connection.Close();
                    return Content((HttpStatusCode)422, "Source VCard doesn't exist");
                }

                externalEntitySource = (int)readerCard1["external_entity_id"];

                readerCard1.Close();

                SqlDataReader readerCard2 = commandSearchVCard2.ExecuteReader();

                int externalEntityDestiny;

                if (!readerCard2.Read())
                {
                    readerCard2.Close();
                    connection.Close();
                    return Content((HttpStatusCode)422, "Destiny VCard doesn't exist");
                }

                externalEntityDestiny = (int)readerCard2["external_entity_id"];

                readerCard2.Close();

                SqlCommand commandGetEndpointSource = new SqlCommand("SELECT * FROM ExternalEntities WHERE id=@id", connection);
                commandGetEndpointSource.Parameters.AddWithValue("@id", externalEntitySource);
                SqlCommand commandGetEndpointDestiny = new SqlCommand("SELECT * FROM ExternalEntities WHERE id=@id", connection);
                commandGetEndpointDestiny.Parameters.AddWithValue("@id", externalEntityDestiny);
                
                SqlDataReader readerEntitySource = commandGetEndpointSource.ExecuteReader();

                string endpointSource;

                if (!readerEntitySource.Read())
                {
                    readerEntitySource.Close();
                    connection.Close();
                    return Content((HttpStatusCode)422, "Error finding Source Financial Entity endpoint");
                }

                endpointSource = (string)readerEntitySource["endpoint"];

                readerEntitySource.Close();

                SqlDataReader readerEntityDestiny = commandGetEndpointDestiny.ExecuteReader();

                string endpointDestiny;

                if (!readerEntityDestiny.Read())
                {
                    readerEntityDestiny.Close();
                    connection.Close();
                    return Content((HttpStatusCode)422, "Error finding Destiny Financial Entity endpoint");
                }

                endpointDestiny = (string)readerEntityDestiny["endpoint"];

                readerEntityDestiny.Close();

                using (HttpClient client = new HttpClient())
                {
                    HttpContent contentSource = new StringContent(JsonConvert.SerializeObject(transaction), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseSource = await client.PostAsync(endpointSource + "api/transactions", contentSource);
                    string responseSourceMessage = await responseSource.Content.ReadAsStringAsync();

                    if (responseSource.StatusCode != HttpStatusCode.OK)
                    {
                        connection.Close();
                        return Content((HttpStatusCode)responseSource.StatusCode, "An error occurred in the external entity associated with the transaction creator vcard: " + responseSourceMessage.Trim('"'));
                    }

                    TransactionPost transactionPair = new TransactionPost()
                    {
                        VCard = transaction.Payment_reference,
                        Value = transaction.Value,
                        Type = transaction.Type == 'C' ? 'D' : 'C',
                        Category_id = 0,
                        Description = null,
                        Payment_reference = transaction.VCard,
                    };

                    HttpContent contentDestiny = new StringContent(JsonConvert.SerializeObject(transactionPair), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseDestiny = await client.PostAsync(endpointDestiny + "api/transactions", contentDestiny);
                    string responseDestinyMessage = await responseDestiny.Content.ReadAsStringAsync();
                    
                    if (responseDestiny.StatusCode != HttpStatusCode.OK)
                    {
                        connection.Close();
                        return Content((HttpStatusCode)responseDestiny.StatusCode, "An error occurred in the external entity associated with the transaction receiver vcard: " + responseDestinyMessage.Trim('"'));
                    }
                }

                MqttClient mqttClient = new MqttClient("127.0.0.1");

                mqttClient.Connect(Guid.NewGuid().ToString());

                if (mqttClient.IsConnected)
                {
                    byte[] generalMsg = Encoding.UTF8.GetBytes($"Transaction of {transaction.Value}€ from {transaction.VCard} to {transaction.Payment_reference}");
                    mqttClient.Publish("operations", generalMsg);
                    byte[] destinyMsg = Encoding.UTF8.GetBytes($"You received a transaction of {transaction.Value}€ from {transaction.VCard}");
                    mqttClient.Publish(transaction.Payment_reference, destinyMsg);
                }
                 
                XmlHelper.WriteLog("transaction", $"Transaction with success between vcard {transaction.VCard} and {transaction.Payment_reference}");
                if (mqttClient.IsConnected)
                    mqttClient.Disconnect();
                return Ok(transaction);
            }
            catch (Exception e)
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                XmlHelper.WriteLog("transaction", $"Transaction between vcard {transaction.VCard} and {transaction.Payment_reference} failed");
                return InternalServerError(e);
            }
        }
    }
}
