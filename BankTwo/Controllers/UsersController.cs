using BankTwo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web.Http;
using System.Security.Cryptography;
using System.Text;

namespace BankTwo.Controllers
{
    public class UsersController : ApiController
    {
        string connectionString = Properties.Settings.Default.BankTwoDBConnection;


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
                    string photoUserBase64 = null;
                    if (reader["photo_url"] == DBNull.Value)
                    {
                        photoUserBase64 = null;
                    }
                    else
                    {
                        Image image = Image.FromFile((string)reader["photo_url"]);
                        byte[] byteArray;
                        using (var ms = new MemoryStream())
                        {
                            image.Save(ms, ImageFormat.Jpeg);
                            byteArray = ms.ToArray();
                        }
                        photoUserBase64 = Convert.ToBase64String(byteArray);
                    }

                    User user = new User
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["name"],
                        Photo = photoUserBase64,
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
                    string photoUserBase64 = null;
                    if (reader["photo_url"] == DBNull.Value)
                    {
                        photoUserBase64 = null;
                    }
                    else
                    {
                        Image image = Image.FromFile((string)reader["photo_url"]);
                        byte[] byteArray;
                        using (var ms = new MemoryStream())
                        {
                            image.Save(ms, ImageFormat.Jpeg);
                            byteArray = ms.ToArray();
                        }
                        photoUserBase64 = Convert.ToBase64String(byteArray);
                    }

                    user = new User
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["name"],
                        Email = (string)reader["email"],
                        Photo = photoUserBase64,
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
            string path = null;
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
                command.Parameters.AddWithValue("@phone_number", user.Phone_number);

                if (user.Photo == null)
                {
                    command.Parameters.AddWithValue("@photo_url", null);
                }
                else
                {
                    byte[] imagebytes = Convert.FromBase64String(user.Photo);
                    Stream stream = new MemoryStream(imagebytes);
                    Image image = Image.FromStream(stream);

                    path = AppDomain.CurrentDomain.BaseDirectory + "UserPictures\\" + user.Phone_number + ".jpg";
                    image.Save(path);
                    command.Parameters.AddWithValue("@photo_url", path);
                }

                int numeroRegistos = command.ExecuteNonQuery();

                connection.Close();

                if (numeroRegistos > 0)
                {
                    return Ok();
                }
                return BadRequest();


            }
            catch (Exception)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
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
            string path = null;
            string oldPath = null;
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
                command.Parameters.AddWithValue("@phone_number", user.Phone_number ?? (string)reader["phone_number"]);


                if (user.Photo == null)
                {
                    command.Parameters.AddWithValue("@photo_url", (string)reader["photo_url"]);
                }
                else
                {
                    oldPath = (string)reader["photo_url"];

                    byte[] imagebytes = Convert.FromBase64String(user.Photo);
                    Stream stream = new MemoryStream(imagebytes);
                    Image image = Image.FromStream(stream);

                    path = AppDomain.CurrentDomain.BaseDirectory + "UserPictures\\" + user.Phone_number + ".jpg";
                    image.Save(path);
                    command.Parameters.AddWithValue("@photo_url", path);
                }

                reader.Close();

                int numeroRegistos = command.ExecuteNonQuery();

                connection.Close();

                if (numeroRegistos > 0)
                {
                    if (File.Exists(oldPath))
                    {
                        File.Delete(oldPath);
                    }
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception e)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
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

                    newHashedPassword = userCredentials.Password != null ? Convert.ToBase64String(mySHA256.ComputeHash(Encoding.UTF8.GetBytes(userCredentials.Password))) : null;
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
            string path = null;

            try
            {
                connection = new SqlConnection(connectionString);
                string sql = "SELECT photo_url FROM Users WHERE Id = @id";
                connection.Open();

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    path = (string)reader["photo_url"];
                }
                else
                {
                    return NotFound();
                }
                reader.Close();
                connection.Close();


                connection = new SqlConnection(connectionString);

                sql = "DELETE FROM Users WHERE Id = @id";

                connection.Open();

                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);

                int numeroRegistos = command.ExecuteNonQuery();

                connection.Close();

                if (numeroRegistos > 0)
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    return Ok();
                }
                return NotFound();

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
