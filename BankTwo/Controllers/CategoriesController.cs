using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BankTwo.Models;

namespace BankTwo.Controllers
{
    public class CategoriesController : ApiController
    {
        string connectionString = Properties.Settings.Default.BankTwoDBConnection;

        public IHttpActionResult GetAllCategories()
        {
            List<Category> categories = new List<Category>();

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Categories", conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Category category = new Category
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["name"],
                        Type = Convert.ToChar(reader["type"]),
                        User_id = (int)reader["user_id"]
                    };
                    categories.Add(category);
                }

                reader.Close();

                conn.Close();
            }
            catch (Exception e)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return InternalServerError(e);


            }

            return Ok(categories);

        }

        [Route("api/users/{idUser:int}/categories")]
        public IHttpActionResult GetCategoriesOfUser(int idUser)
        {
            List<Category> categories = new List<Category>();

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Categories WHERE user_id = @idUser", conn);
                command.Parameters.AddWithValue("@idUser", idUser);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Category category = new Category
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["name"],
                        Type = Convert.ToChar(reader["type"]),
                        User_id = (int)reader["user_id"]
                    };
                    categories.Add(category);
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

            return Ok(categories);

        }

        public IHttpActionResult PostCategory([FromBody] Category category)
        {
            SqlConnection connection = null;

            try
            {

                connection = new SqlConnection(connectionString);
                string sql = "INSERT INTO Categories VALUES (@name, @type, @user_id)";
                connection.Open();

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@name", category.Name);
                command.Parameters.AddWithValue("@type", category.Type);
                command.Parameters.AddWithValue("@user_id", category.User_id);

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
        public IHttpActionResult PutCategory(int id, [FromBody] Category category)
        {

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);


                string sql = "UPDATE Categories SET Name = @name, Type = @type WHERE id = @id";

                connection.Open();
                SqlCommand commandSearch = new SqlCommand("SELECT * FROM Categories WHERE id = @id", connection);
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
                command.Parameters.AddWithValue("@name", category.Name ?? (string)reader["name"]);
                command.Parameters.AddWithValue("@type", category.Type.ToString() == "" ? Convert.ToChar(reader["type"]) : category.Type);

                reader.Close();

                int numeroRegistos = command.ExecuteNonQuery();

                connection.Close();

                if (numeroRegistos > 0)
                {
                    return Ok(category);
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

        public IHttpActionResult DeleteCategory(int id)
        {

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);

                string sql = "DELETE FROM Categories WHERE Id = @id";

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
