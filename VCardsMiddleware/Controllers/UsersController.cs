using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;
using Newtonsoft.Json;
using uPLibrary.Networking.M2Mqtt;
using VCardsMiddleware.Models;

namespace VCardsMiddleware.Controllers
{
    public class UsersController : ApiController
    {

        string connectionString = Properties.Settings.Default.DBConnString;

        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> PostUser([FromBody] User user)
        {
            if (user == null)
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

                command.Parameters.AddWithValue("@id", user.External_entity_id);

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
                var payload = "{\"Name\": \"" + user.Name + "\",\"Email\": \"" + user.Email + "\",\"Phone_number\": \"" + user.Phone_number + "\",\"Password\": \"" + user.Password + "\",\"Confirmation_code\": \"" + user.Confirmation_code + "\"}";
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(endpoint + "api/users", content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MqttClient mqttClient = new MqttClient("127.0.0.1");

                    mqttClient.Connect(Guid.NewGuid().ToString());

                    if (mqttClient.IsConnected)
                    {
                        byte[] generalMsg = Encoding.UTF8.GetBytes($"User {user.Name} created in External Entity {user.External_entity_id}");
                        mqttClient.Publish("operations", generalMsg);
                    }
                    XmlHelper.WriteLog("userCreated", $"An administrator created a user ({user.Name}) in External Entity with id {user.External_entity_id}");
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
                XmlHelper.WriteLog("userCreated", $"An administrator tried to create a user ({user.Name}) in External Entity with id {user.External_entity_id} but the operation failed");
                return InternalServerError();
            }
        }
    }
}
