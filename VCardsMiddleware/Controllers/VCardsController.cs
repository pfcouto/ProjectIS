using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VCardsMiddleware.Models;

namespace VCardsMiddleware.Controllers
{
    public class VCardsController : ApiController
    {
        string connectionString = Properties.Settings.Default.DBConnString;

        [Authorize(Roles = "admin")]
        public IHttpActionResult GetAllUsers()
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

    }
}
