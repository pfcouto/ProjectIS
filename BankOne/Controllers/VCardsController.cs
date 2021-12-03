using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using BankOne.Models;

namespace BankOne.Controllers
{
    public class VCardsController : ApiController
    {
        string connectionString = Properties.Settings.Default.BankOneDBConnection;


        public IHttpActionResult GetAllVCards()
        {
            List<VCard> vCards = new List<VCard>();

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM VCards", conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    VCard vCard = new VCard
                    {
                        Phone_number = Convert.ToInt32(reader["phone_number"]),
                        User_id = Convert.ToInt32(reader["user_id"]),
                        Balance = reader.GetDecimal(1),
                        Max_debit = reader.GetDecimal(2),
                        Earning_percentage = reader.GetDecimal(3),
                    };
                    vCards.Add(vCard);
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

            return Ok(vCards);

        }

        [Route("api/vcards/{phoneNumber}")]
        public IHttpActionResult GetVCardsByPhoneNumber(int phoneNumber)
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM VCards WHERE phone_number = @phone_number", conn);
                command.Parameters.AddWithValue("@phone_number", phoneNumber);
                SqlDataReader reader = command.ExecuteReader();
                VCard vCard = null;

                if (reader.Read())
                {
                    vCard = new VCard
                    {
                        Phone_number = Convert.ToInt32(reader["phone_number"]),
                        User_id = Convert.ToInt32(reader["user_id"]),
                        Balance = reader.GetDecimal(1),
                        Max_debit = reader.GetDecimal(2),
                        Earning_percentage = reader.GetDecimal(3),
                    };
                }

                reader.Close();
                conn.Close();

                if (vCard != null)
                {
                    return Ok(vCard);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return BadRequest(e.Message);
            }
        }

        [Route("api/users/{userId:int}/vcards")]
        public IHttpActionResult GetVCardsOfUser(int userId)
        {
            List<VCard> vCards = new List<VCard>();

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM VCards WHERE user_id = @user_id", conn);
                command.Parameters.AddWithValue("@user_id", userId);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    VCard vCard = new VCard
                    {
                        Phone_number = Convert.ToInt32(reader["phone_number"]),
                        User_id = Convert.ToInt32(reader["user_id"]),
                        Balance = reader.GetDecimal(1),
                        Max_debit = reader.GetDecimal(2),
                        Earning_percentage = reader.GetDecimal(3),
                    };
                    vCards.Add(vCard);
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
                return BadRequest(e.Message);
            }

            return Ok(vCards);
        }
        public IHttpActionResult PostVCard([FromBody] VCard vCard)
        {
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                string sql = "INSERT INTO VCards VALUES (@phone_number, @balance, @max_debit, @earning_percentage, @user_id)";
                connection.Open();

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@phone_number", vCard.Phone_number);
                command.Parameters.AddWithValue("@user_id", vCard.User_id);
                command.Parameters.AddWithValue("@balance", vCard.Balance);
                command.Parameters.AddWithValue("@max_debit", vCard.Max_debit);
                command.Parameters.AddWithValue("@earning_percentage", vCard.Phone_number);

                int nRegistos = command.ExecuteNonQuery();
                connection.Close();

                if (nRegistos > 0)
                {
                    return Ok();
                }
                return BadRequest();

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

        public IHttpActionResult PutVCard(int phoneNumber, [FromBody] VCard vCard)
        {

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand commandSearch = new SqlCommand("SELECT * FROM VCards WHERE phone_number = @phone_number", connection);
                commandSearch.Parameters.AddWithValue("@phone_number", phoneNumber);

                string sql = "UPDATE VCards SET balance = @balance, max_debit = @max_debit, earning_percentage = @earning_percentage WHERE phone_number = @phone_number";
                SqlCommand command = new SqlCommand(sql, connection);


                SqlDataReader reader = commandSearch.ExecuteReader();

                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return NotFound();
                }

                command.Parameters.AddWithValue("@phone_number", phoneNumber);
                command.Parameters.AddWithValue("@balance", vCard.Balance.Equals(null) ? reader.GetDecimal(1) : vCard.Balance);
                command.Parameters.AddWithValue("@max_debit", vCard.Max_debit.Equals(null) ? reader.GetDecimal(2) : vCard.Max_debit);
                command.Parameters.AddWithValue("@earning_percentage", vCard.Earning_percentage.Equals(null) ? reader.GetDecimal(3) : vCard.Earning_percentage);

                reader.Close();

                int numeroRegistos = command.ExecuteNonQuery();
                connection.Close();

                if (numeroRegistos > 0)
                {
                    return Ok();
                }
                return NotFound();


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



        public IHttpActionResult DeleteVCard(int phoneNumber)
        {

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();

                string sql = "DELETE FROM VCards WHERE phone_number = @phone_number AND balance = 0";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@phone_number", phoneNumber);

                int numeroRegistos = command.ExecuteNonQuery();

                connection.Close();

                if (numeroRegistos > 0)
                {
                    return Ok();
                }
                return NotFound();

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
