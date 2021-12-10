using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using VCardsMiddleware.Models;

namespace VCardsMiddleware.Controllers
{
    public class TransactionsController : ApiController
    {

        List<Transaction> transactions = new List<Transaction>();
        string connectionString = Properties.Settings.Default.DBConnString;

        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> GetTansactions(string type = null, string dateFrom = null, string dateTo = null)
        {
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                string sql = "SELECT endpoint FROM ExternalEntities";
                connection.Open();

                SqlCommand command = new SqlCommand(sql, connection);

                SqlDataReader reader = command.ExecuteReader();
                string endpoint;

                HttpClient client = new HttpClient();

                while (reader.Read())
                {
                    endpoint = (string)reader["endpoint"];
                    endpoint += "api/transactions";

                    if (!string.IsNullOrEmpty(type) || !string.IsNullOrEmpty(dateFrom) || !string.IsNullOrEmpty(dateTo))
                    {
                        endpoint += '?';
                    }

                    if (!string.IsNullOrEmpty(type))
                    {
                        endpoint += "type=" + type;
                    }

                    if (!string.IsNullOrEmpty(dateFrom) && !string.IsNullOrEmpty(dateTo))
                    {
                        if (!type.Equals(null))
                        {
                            endpoint += "&dateFrom=" + dateFrom + "&dateTo" + dateTo;
                        }
                    }

                    HttpResponseMessage response = await client.GetAsync(endpoint);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        transactions = JsonConvert.DeserializeObject<List<Transaction>>(responseBody);
                    }
                }

                return Ok(transactions);

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
