using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VCardsMiddleware.Models;

namespace VCardsMiddleware.Controllers
{
    public class ExternalEntitiesController : ApiController
    {
        string connectionString = Properties.Settings.Default.DBConnString;
        
        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> GetAll()
        {
            List<ExternalEntity> externalEntities = new List<ExternalEntity>();

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM ExternalEntities", conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ExternalEntity externalEntity = new ExternalEntity
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["name"],
                        Endpoint = (string)reader["endpoint"],
                        Status = await GetEndpointStatus((string)reader["endpoint"])
                    };
                    externalEntities.Add(externalEntity);
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
            return Ok(externalEntities);
        }

        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> PostExternalEntity([FromBody] ExternalEntity externalEntity)
        {
            if (externalEntity == null)
            {
                return BadRequest();
            }

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                string sql = "INSERT INTO ExternalEntities VALUES (@name, @endpoint)";
                connection.Open();

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@name", externalEntity.Name);
                command.Parameters.AddWithValue("@endpoint", externalEntity.Endpoint);

                int numeroRegistos = command.ExecuteNonQuery();

                if (numeroRegistos > 0)
                {
                    SqlCommand commandSearch = new SqlCommand("SELECT Id FROM ExternalEntities WHERE endpoint = @endpoint", connection);
                    commandSearch.Parameters.AddWithValue("@endpoint", externalEntity.Endpoint);
                    SqlDataReader reader = commandSearch.ExecuteReader();

                    if (reader.Read())
                    {
                        externalEntity.Id = (int)reader["Id"];
                    }

                    externalEntity.Status = await GetEndpointStatus(externalEntity.Endpoint);

                    connection.Close();
                    return Ok(externalEntity);
                }
                else
                {
                    connection.Close();
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

        private async Task<char> GetEndpointStatus(string endpoint)
        {
            char status = '0';
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(endpoint + "api/status");

                    if (response.StatusCode == HttpStatusCode.OK)
                        status = '1';
                } catch (HttpRequestException)
                {
                }
            }
            return status;
        }
        public IHttpActionResult Delete(int id)
        {

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);

                string sql = "DELETE FROM ExternalEntities WHERE Id = @id";

                connection.Open();

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);

                int numeroRegistos = command.ExecuteNonQuery();

                connection.Close();

                if (numeroRegistos > 0)
                {
                    return Ok(id);
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
