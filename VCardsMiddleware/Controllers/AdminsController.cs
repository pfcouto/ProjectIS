using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using VCardsMiddleware.Models;

namespace VCardsMiddleware.Controllers
{
    public class AdminsController : ApiController
    {
        string connectionString = Properties.Settings.Default.DBConnString;
       
        [Authorize(Roles = "admin")]  
        public IHttpActionResult PostUser([FromBody] Admin admin)
        {
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                string sql = "INSERT INTO Admins VALUES (@email, @password, @name)";
                connection.Open();

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@name", admin.Name);
                command.Parameters.AddWithValue("@email", admin.Email);
                
                using (SHA256 mySHA256 = SHA256.Create())
                {
                    command.Parameters.AddWithValue("@password", Convert.ToBase64String(mySHA256.ComputeHash(Encoding.UTF8.GetBytes(admin.Password))));
                }

                int numeroRegistos = command.ExecuteNonQuery();

                connection.Close();

                if (numeroRegistos > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception e)
            {

                if (connection.State == System.Data.ConnectionState.Open)
                {

                    connection.Close();
                }
                return InternalServerError(e);
            }
        }
    }
}
