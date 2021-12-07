using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
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
        public IHttpActionResult GetAllAdmins()
        {
            List<Admin> admins = new List<Admin>();

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Admins", conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Admin admin = new Admin
                    {
                        Name = (string)reader["name"],
                        Email = (string)reader["email"],
                        Enabled = char.Parse((string)reader["enabled"])
                    };
                    admins.Add(admin);
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
            return Ok(admins);
        }
       
        [Authorize(Roles = "admin")]
        [Route("api/admins/me")]
        public IHttpActionResult GetMe()
        {
            var identity = (ClaimsIdentity)User.Identity;
           
            Admin admin = new Admin
            {
                Name = identity.FindFirst("name").Value,
                Email = identity.FindFirst("email").Value
            };
            
            return Ok(admin);
        }

        [Authorize(Roles = "admin")]
        [Route("api/admins/me")]
        public IHttpActionResult PatchPassword([FromBody] AdminPassword adminPassword)
        {
            SqlConnection connection = null;
            var identity = (ClaimsIdentity)User.Identity;
            string email = identity.FindFirst("email").Value;

            try
            {
                connection = new SqlConnection(connectionString);

                string sql = "UPDATE Admins SET password = @password WHERE email = @email";

                connection.Open();
                SqlCommand commandSearch = new SqlCommand("SELECT * FROM Admins WHERE email = @email", connection);
                commandSearch.Parameters.AddWithValue("@email", email);

                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = commandSearch.ExecuteReader();

                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return NotFound();
                }

                string hashedPassword;
                string newHashedPassword;

                using (SHA256 mySHA256 = SHA256.Create())
                {
                    hashedPassword =
                        Convert.ToBase64String(mySHA256.ComputeHash(Encoding.UTF8.GetBytes(adminPassword.OldPassword)));
                    
                    newHashedPassword = adminPassword.NewPassword != null ? Convert.ToBase64String(mySHA256.ComputeHash(Encoding.UTF8.GetBytes(adminPassword.NewPassword))) : null;
                }

                if (hashedPassword != (string)reader["password"])
                {
                    reader.Close();
                    connection.Close();
                    return Unauthorized();
                }

                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", newHashedPassword ?? (string)reader["password"]);

                reader.Close();

                int numeroRegistos = command.ExecuteNonQuery();

                connection.Close();

                if (numeroRegistos > 0)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }

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
        [Route("api/admins/{email}/enabled")]
        public IHttpActionResult PatchEnabled(string email, [FromBody] string enabled)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);

                string sql = "UPDATE Admins SET enabled = @enabled WHERE email = @email";

                connection.Open();
                SqlCommand commandSearch = new SqlCommand("SELECT * FROM Admins WHERE email = @email", connection);
                commandSearch.Parameters.AddWithValue("@email", email);

                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = commandSearch.ExecuteReader();

                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return NotFound();
                }

                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@enabled", char.Parse(enabled));

                reader.Close();

                int numeroRegistos = command.ExecuteNonQuery();

                connection.Close();

                if (numeroRegistos > 0)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
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
        
        [Authorize(Roles = "admin")]  
        public IHttpActionResult PostAdmin([FromBody] Admin admin)
        {
            if (admin == null)
            {
                return BadRequest();
            }

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                string sql = "INSERT INTO Admins VALUES (@email, @password, @name, @enabled)";
                connection.Open();

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@name", admin.Name);
                command.Parameters.AddWithValue("@email", admin.Email);
                command.Parameters.AddWithValue("@enabled", '1');
                
                using (SHA256 mySHA256 = SHA256.Create())
                {
                    command.Parameters.AddWithValue("@password", Convert.ToBase64String(mySHA256.ComputeHash(Encoding.UTF8.GetBytes(admin.Password))));
                }

                int numeroRegistos = command.ExecuteNonQuery();

                connection.Close();

                if (numeroRegistos > 0)
                {
                    admin.Enabled = '1';
                    return Ok(admin);
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
