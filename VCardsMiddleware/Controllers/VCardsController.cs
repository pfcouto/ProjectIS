using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    }
}
