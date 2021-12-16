using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using BankOne.Models;
using uPLibrary.Networking.M2Mqtt;
using VCardsMiddleware.Models;

namespace VCardsMiddleware.Controllers
{
    public class VCardsController : ApiController
    {
        string connectionString = Properties.Settings.Default.DBConnString;

        [Authorize(Roles = "admin")]
        public IHttpActionResult GetAllVCards()
        {
            List<VCardExternalEntity> users = new List<VCardExternalEntity>();

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM VCards", conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    VCardExternalEntity vCard = new VCardExternalEntity
                    {
                        PhoneNumber = (string)reader["phone_number"],
                        ExternalEntityId = (int)reader["external_entity_id"],
                    };
                    users.Add(vCard);
                }

                reader.Close();

                conn.Close();
            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }

                return InternalServerError();


            }

            return Ok(users);
        }


        [Authorize(Roles = "admin")]
        [Route("api/VCards/{phoneNumber}/balance")]
        public async Task<IHttpActionResult> GetBalance(string phoneNumber)
        {
            SqlConnection conn = null;
            decimal balance = 0;
            string endpoint = null;


            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM VCards WHERE phone_number = @phoneNumber", conn);
                command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                SqlDataReader reader = command.ExecuteReader();
                int id;
                if (reader.Read())
                {
                    id = (int)reader["external_entity_id"];
                }
                else
                {
                    throw new Exception();
                }

                reader.Close();


                //get endpoint
                conn = new SqlConnection(connectionString);
                conn.Open();

                command = new SqlCommand("SELECT endpoint FROM ExternalEntities WHERE Id = @Id", conn);
                command.Parameters.AddWithValue("@Id", id);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    endpoint = (string)reader["endpoint"];
                }
                else
                {
                    throw new Exception();
                }

                reader.Close();


                conn.Close();
            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }

                return InternalServerError();
            }


            //request balance from entity
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(endpoint + "api/vcards/" + phoneNumber + "/balance");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return InternalServerError();
            }

            var content = await response.Content.ReadAsStringAsync();

            //return Ok(content);
            return Ok(Decimal.Parse(content, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture));
        }

        public async Task<IHttpActionResult> PostVCard([FromBody] VCardUserPassword vCard)
        {
            if (vCard == null)
            {
                return BadRequest();
            }

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                string sql = "SELECT endpoint FROM ExternalEntities WHERE id = @id";
                connection.Open();

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@id", vCard.External_entity_id);

                SqlDataReader reader = command.ExecuteReader();
                string endpoint;

                if (reader.Read())
                {
                    endpoint = (string)reader["endpoint"];
                }
                else
                {
                    connection.Close();
                    return BadRequest();
                }

                HttpClient client = new HttpClient();
                var payload = "{\"Phone_number\": \"" + vCard.Phone_number + "\",\"User_password\": \"" + vCard.User_password + "\",\"User_id\": \"" + vCard.User_id + "\",\"Max_debit\": \"" + vCard.Max_debit.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "\",\"Earning_percentage\": \"" + vCard.Earning_percentage.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "\"}";
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(endpoint + "api/vcards", content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //register in BD
                    try
                    {
                        connection = new SqlConnection(connectionString);
                        sql = "INSERT INTO VCards VALUES (@phone_number, @external_entity_id)";
                        connection.Open();

                        command = new SqlCommand(sql, connection);

                        command.Parameters.AddWithValue("@phone_number", vCard.Phone_number);
                        command.Parameters.AddWithValue("@external_entity_id", vCard.External_entity_id);

                        int numeroRegistos = command.ExecuteNonQuery();

                        connection.Close();

                        if (numeroRegistos > 0)
                        {
                            MqttClient mqttClient = new MqttClient("127.0.0.1");

                            mqttClient.Connect(Guid.NewGuid().ToString());

                            if (mqttClient.IsConnected)
                            {
                                byte[] generalMsg = Encoding.UTF8.GetBytes($"VCard with phone number {vCard.Phone_number} was created for user {vCard.User_id}");
                                mqttClient.Publish("operations", generalMsg);
                            }
                            XmlHelper.WriteLog("vcardCreated", $"A VCard with phone number {vCard.Phone_number} was created for user {vCard.User_id} of External Entity {vCard.External_entity_id}");
                            if (mqttClient.IsConnected)
                                mqttClient.Disconnect();
                            return Ok();
                        }
                        return BadRequest();


                    }
                    catch (Exception)
                    {
                        if (connection.State == System.Data.ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        XmlHelper.WriteLog("vcardCreated", $"Failed to create a VCard for user {vCard.User_id} of External Entity {vCard.External_entity_id}");
                        return InternalServerError();
                    }

                }

                return BadRequest();

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
