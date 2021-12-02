using BankOne.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankOne.Controllers
{
    public class UsersController : ApiController
    {
        string connectionString = Properties.Settings.Default.BankOneDBConnection;


        public IEnumerable<User> GetAllUsers()
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
                    // Converter da tabela para objeto Product
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

            }
            return users;

        }
    }
}
