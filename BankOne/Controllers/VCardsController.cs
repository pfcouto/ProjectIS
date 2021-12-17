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
                        Phone_number = (string)reader["phone_number"],
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
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return InternalServerError();
            }

            return Ok(vCards);

        }

        [Route("api/vcards/{phoneNumber}")]
        public IHttpActionResult GetVCardsByPhoneNumber(string phoneNumber)
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
                        Phone_number = (string)reader["phone_number"],
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
                        Phone_number = (string)reader["phone_number"],
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
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return BadRequest();
            }

            return Ok(vCards);
        }
        public IHttpActionResult PostVCard([FromBody] VCardUserPassword vCard)
        {

            if (vCard == null ||
                (!vCard.Max_debit.Equals(null) && vCard.Max_debit < 0) ||
                (!vCard.Earning_percentage.Equals(null) && vCard.Earning_percentage < 0))
            {
                return BadRequest();
            }

            SqlConnection connection = null;

            try
            {
                //verify user and password
                connection = new SqlConnection(connectionString);
                connection.Open();
                string sql = "SELECT password FROM Users WHERE Id = @Id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", vCard.User_id);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string passwordHash = (string)reader[0];
                    byte[] byteNewPasswordHash;
                    string newPasswordHash;
                    using (var sha = new SHA256CryptoServiceProvider())
                    {
                        byteNewPasswordHash = sha.ComputeHash(Encoding.UTF8.GetBytes(vCard.User_password));
                        newPasswordHash = Convert.ToBase64String(byteNewPasswordHash);
                    }

                    if (passwordHash != newPasswordHash)
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    connection.Close();
                    return BadRequest();
                }

                connection.Close();


                connection = new SqlConnection(connectionString);
                connection.Open();
                sql = "INSERT INTO VCards VALUES (@phone_number, @balance, @max_debit, @earning_percentage, @user_id)";

                command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@phone_number", vCard.Phone_number);
                command.Parameters.AddWithValue("@user_id", vCard.User_id);
                command.Parameters.AddWithValue("@balance", 0);
                command.Parameters.AddWithValue("@max_debit", vCard.Max_debit.Equals(null) ? 0 : vCard.Max_debit);
                command.Parameters.AddWithValue("@earning_percentage", vCard.Earning_percentage.Equals(null) ? 0 : vCard.Earning_percentage);

                int nRegistos = command.ExecuteNonQuery();
                connection.Close();

                if (nRegistos > 0)
                {
                    return Ok();
                }
                return BadRequest();

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

        public IHttpActionResult PutVCard(int phoneNumber, [FromBody] VCard vCard)
        {
            if (vCard == null ||
                (!vCard.Max_debit.Equals(null) && vCard.Max_debit < 0) ||
                (!vCard.Earning_percentage.Equals(null) && vCard.Earning_percentage < 0))
            {
                return BadRequest();
            }

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand commandSearch = new SqlCommand("SELECT * FROM VCards WHERE phone_number = @phone_number", connection);
                commandSearch.Parameters.AddWithValue("@phone_number", phoneNumber);

                string sql = "UPDATE VCards SET max_debit = @max_debit, earning_percentage = @earning_percentage WHERE phone_number = @phone_number";
                SqlCommand command = new SqlCommand(sql, connection);


                SqlDataReader reader = commandSearch.ExecuteReader();

                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return NotFound();
                }

                command.Parameters.AddWithValue("@phone_number", phoneNumber);
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
            catch (Exception)
            {

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                return InternalServerError();
            }

        }

        [Route("api/vcards/{phoneNumber}/maxDebit")]
        public IHttpActionResult PatchVCardMaxDebit(string phoneNumber, [FromBody] Decimal maxDebit)
        {
            if (maxDebit.Equals(null) || maxDebit < 0)
            {
                return BadRequest();
            }

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand commandSearch = new SqlCommand("SELECT * FROM VCards WHERE phone_number = @phone_number", connection);
                commandSearch.Parameters.AddWithValue("@phone_number", phoneNumber);

                string sql = "UPDATE VCards SET max_debit = @max_debit WHERE phone_number = @phone_number";
                SqlCommand command = new SqlCommand(sql, connection);


                SqlDataReader reader = commandSearch.ExecuteReader();

                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return NotFound();
                }

                command.Parameters.AddWithValue("@phone_number", phoneNumber);
                command.Parameters.AddWithValue("@max_debit", maxDebit.Equals(null) ? reader.GetDecimal(2) : maxDebit);

                reader.Close();

                int numeroRegistos = command.ExecuteNonQuery();
                connection.Close();

                if (numeroRegistos > 0)
                {
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

        [Route("api/vcards/{phoneNumber}/earningPercentage")]
        public IHttpActionResult PatchVCardEarningPercentage(string phoneNumber, [FromBody] Decimal earningPercentage)
        {
            if (earningPercentage.Equals(null) || earningPercentage < 0)
            {
                return BadRequest();
            }

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand commandSearch = new SqlCommand("SELECT * FROM VCards WHERE phone_number = @phone_number", connection);
                commandSearch.Parameters.AddWithValue("@phone_number", phoneNumber);

                string sql = "UPDATE VCards SET earning_percentage = @earning_percentage WHERE phone_number = @phone_number";
                SqlCommand command = new SqlCommand(sql, connection);


                SqlDataReader reader = commandSearch.ExecuteReader();

                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return NotFound();
                }

                command.Parameters.AddWithValue("@phone_number", phoneNumber);
                command.Parameters.AddWithValue("@earning_percentage", earningPercentage.Equals(null) ? reader.GetDecimal(2) : earningPercentage);

                reader.Close();

                int numeroRegistos = command.ExecuteNonQuery();
                connection.Close();

                if (numeroRegistos > 0)
                {
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
            catch (Exception)
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                return InternalServerError();
            }
        }

        [Route("api/vcards/{phoneNumber}/balanceEarningPercentage")]
        public IHttpActionResult GetVCardBalanceEarningPercentage(string phoneNumber)
        {
            SqlConnection conn = null;
            BalanceEarningPercentage be = null;


            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT balance,earning_percentage FROM VCards WHERE phone_number = @phone_number", conn);
                command.Parameters.AddWithValue("@phone_number", phoneNumber);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    be = new BalanceEarningPercentage()
                    {
                        Balance = reader.GetDecimal(0),
                        EarningPercentage = reader.GetDecimal(1)
                    };
                }
                else
                {
                    throw new Exception();
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
                return BadRequest();
            }

            return Ok(be);
        }
    }
}
