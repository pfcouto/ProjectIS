using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BankOne.Models;

namespace BankOne.Controllers
{
    public class DefaultCategoriesController : ApiController
    {
        string connectionString = Properties.Settings.Default.BankOneDBConnection;

        public IHttpActionResult GetAllDefaultCategories()
        {
            List<DefaultCategory> defaultCategories = new List<DefaultCategory>();

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM DefaultCategories", conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DefaultCategory defaultCategory = new DefaultCategory
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["name"],
                        Type = Convert.ToChar(reader["type"]),
                    };
                    defaultCategories.Add(defaultCategory);
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

            return Ok(defaultCategories);

        }

        public IHttpActionResult PostDefaultCategory([FromBody] DefaultCategory defaultCategory)
        {
            SqlConnection connection = null;

            try
            {

                connection = new SqlConnection(connectionString);
                string sql = "INSERT INTO DefaultCategories VALUES (@name, @type)";
                connection.Open();

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@name", defaultCategory.Name);
                command.Parameters.AddWithValue("@type", defaultCategory.Type);

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
        public IHttpActionResult PutDefaultCategory(int id, [FromBody] DefaultCategory defaultCategory)
        {

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);


                string sql = "UPDATE DefaultCategories SET Name = @name, Type = @type WHERE id = @id";

                connection.Open();
                SqlCommand commandSearch = new SqlCommand("SELECT * FROM DefaultCategories WHERE id = @id", connection);
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
                command.Parameters.AddWithValue("@name", defaultCategory.Name ?? (string)reader["name"]);
                command.Parameters.AddWithValue("@type", defaultCategory.Type.ToString() == "" ? Convert.ToChar(reader["type"]) : defaultCategory.Type);
                reader.Close();

                int numeroRegistos = command.ExecuteNonQuery();

                connection.Close();

                if (numeroRegistos > 0)
                {
                    return Ok(defaultCategory);
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

        public IHttpActionResult DeleteDefaultCategory(int id)
        {

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);

                string sql = "DELETE FROM DefaultCategories WHERE Id = @id";

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
