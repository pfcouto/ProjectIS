using BankOne.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web.Http;
using System.Security.Cryptography;
using System.Text;

namespace BankOne.Controllers
{
    public class UsersController : ApiController
    {
        string connectionString = Properties.Settings.Default.BankOneDBConnection;


        public IHttpActionResult GetAllUsers()
        {
            List<User> users = new List<User>();

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Users", conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["name"],
                        Photo_url = reader["photo_url"] == DBNull.Value ? "" : (string)reader["photo_url"],
                        Password = (string)reader["password"],
                        Confirmation_code = (string)reader["confirmation_code"],
                        Phone_number = (string)reader["phone_number"]
                    };
                    users.Add(user);
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

        public IHttpActionResult GetById(int id)
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE id = @id", conn);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                User user = null;

                if (reader.Read())
                {
                    user = new User
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["name"],
                        Email = (string)reader["email"],
                        Photo_url = reader["photo_url"] == DBNull.Value ? "" : (string)reader["photo_url"],
                        Phone_number = (string)reader["phone_number"]
                    };
                }

                reader.Close();

                conn.Close();

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                
                if (conn.State == System.Data.ConnectionState.Open)
                {

                    conn.Close();

                }
                return BadRequest();

            }

        }
        
        public IHttpActionResult PostUser([FromBody] User user)
        {
            SqlConnection connection = null;

            try
            {

                connection = new SqlConnection(connectionString);
                string sql = "INSERT INTO Users VALUES (@name, @email, @password, @photo_url, @confirmation_code, @phone_number)";
                connection.Open();

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.AddWithValue("@email", user.Email);
                
                using (SHA256 mySHA256 = SHA256.Create())
                {
                    command.Parameters.AddWithValue("@password", Convert.ToBase64String(mySHA256.ComputeHash(Encoding.UTF8.GetBytes(user.Password))));
                    command.Parameters.AddWithValue("@confirmation_code", Convert.ToBase64String(mySHA256.ComputeHash(Encoding.UTF8.GetBytes(user.Confirmation_code))));
                }

                command.Parameters.AddWithValue("@photo_url", user.Photo_url ?? "");
                command.Parameters.AddWithValue("@phone_number", user.Phone_number);


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
            catch (Exception)
            {

                if (connection.State == System.Data.ConnectionState.Open)
                {

                    connection.Close();
                }
                return InternalServerError();
            }
        }

        public IHttpActionResult PutUser(int id, [FromBody] User user)
        {

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);


                string sql = "UPDATE Users SET Name = @name, Email = @email, Photo_url = @photo_url, Phone_number = @phone_number WHERE id = @id";

                connection.Open();
                SqlCommand commandSearch = new SqlCommand("SELECT * FROM Users WHERE id = @id", connection);
                commandSearch.Parameters.AddWithValue("@id", id);

                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = commandSearch.ExecuteReader();

                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return NotFound();
                }

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", user.Name ?? (string)reader["name"]);
                command.Parameters.AddWithValue("@email", user.Email ?? (string)reader["email"]);
                command.Parameters.AddWithValue("@photo_url", user.Photo_url ?? (string)reader["photo_url"]);
                command.Parameters.AddWithValue("@phone_number", user.Phone_number ?? (string)reader["phone_number"]);

                reader.Close();

                int numeroRegistos = command.ExecuteNonQuery();

                connection.Close();

                if (numeroRegistos > 0)
                {
                    return Ok(user);
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

        public IHttpActionResult PatchUser(int id, [FromBody] UserCredentials userCredentials)
        {

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);


                string sql = "UPDATE Users SET password = @password, confirmation_code = @confirmation_code WHERE id = @id";

                connection.Open();
                SqlCommand commandSearch = new SqlCommand("SELECT * FROM Users WHERE id = @id", connection);
                commandSearch.Parameters.AddWithValue("@id", id);

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
                string newConfirmationCode;

                using (SHA256 mySHA256 = SHA256.Create())
                {
                    hashedPassword =
                        Convert.ToBase64String(mySHA256.ComputeHash(Encoding.UTF8.GetBytes(userCredentials.OldPassword)));
                    
                    newHashedPassword = userCredentials.Password != null ? Convert.ToBase64String(mySHA256.ComputeHash(Encoding.UTF8.GetBytes(userCredentials.Password))) : null ;
                    newConfirmationCode = userCredentials.ConfirmationCode != null ? Convert.ToBase64String(mySHA256.ComputeHash(Encoding.UTF8.GetBytes(userCredentials.ConfirmationCode))) : null;
                }

                if (hashedPassword != (string)reader["password"])
                {
                    reader.Close();
                    connection.Close();
                    return Unauthorized();
                }

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@password", newHashedPassword ?? (string)reader["password"]);
                command.Parameters.AddWithValue("@confirmation_code", newConfirmationCode ?? (string)reader["confirmation_code"]);

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

        public IHttpActionResult Delete(int id)
        {

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);

                string sql = "DELETE FROM Users WHERE Id = @id";

                connection.Open();

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);

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
    }

}
