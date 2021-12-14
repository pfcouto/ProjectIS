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

        [Authorize(Roles = "admin")]
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
    }
}
