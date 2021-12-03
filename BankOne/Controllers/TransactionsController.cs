using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using BankOne.Models;
using System.Security.Cryptography;
using System.Text;

namespace BankOne.Controllers
{
    public class TransactionsController : ApiController
    {
        readonly string connectionString = Properties.Settings.Default.BankOneDBConnection;

        [Route("api/vcards/{phone_number}/transactions")]
        public IHttpActionResult GetTransactionsOfVcard(string phone_number)
        {
            List<VCardTransaction> transactions = new List<VCardTransaction>();

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM VCardTransactions WHERE vcard=@vcard", conn);
                command.Parameters.AddWithValue("@vcard", phone_number);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    VCardTransaction transaction = new VCardTransaction
                    {
                        Id = (int)reader["Id"],
                        VCard = (string)reader["vcard"],
                        Date = (DateTime)reader["date"],
                        Type = char.Parse((string)reader["type"]),
                        Value = reader.GetDecimal(4),
                        Category_id = reader["category_id"] == DBNull.Value ? 0 : (int)reader["category_id"],
                        Description = reader["description"] == DBNull.Value ? null : (string)reader["description"],
                    };

                    transactions.Add(transaction);
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

            return Ok(transactions);
        }
        
        [Route("api/users/{id:int}/transactions")]
        public IHttpActionResult GetTransactionsOfUser(int id)
        {
            List<VCardTransaction> transactions = new List<VCardTransaction>();

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM VCardTransactions WHERE vcard IN (SELECT phone_number FROM VCards WHERE user_id=@id)", conn);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    VCardTransaction transaction = new VCardTransaction
                    {
                        Id = (int)reader["Id"],
                        VCard = (string)reader["vcard"],
                        Date = (DateTime)reader["date"],
                        Type = char.Parse((string)reader["type"]),
                        Value = reader.GetDecimal(4),
                        Category_id = reader["category_id"] == DBNull.Value ? 0 : (int)reader["category_id"],
                        Description = reader["description"] == DBNull.Value ? null : (string)reader["description"],
                    };

                    transactions.Add(transaction);
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

            return Ok(transactions);
        }

        public IHttpActionResult PostTransaction(string confirmation_code, [FromBody] VCardTransaction transaction)
        {
            if (transaction == null)
                return BadRequest();

            if (transaction.Value <= 0)
                return Content((HttpStatusCode)422, "Invalid transaction value");
            
            if (transaction.Type != 'D' && transaction.Type != 'C')
                return Content((HttpStatusCode)422, "Invalid type of transaction");
            
            if (transaction.Description != null && transaction.Description.Length > 255)
                return Content((HttpStatusCode)422, "Invalid description (Must be smaller than 255 characters");

            SqlConnection connection = null;
            SqlTransaction sqlTransaction = null;

            try
            {
                connection = new SqlConnection(connectionString);
                
                connection.Open();
                
                SqlCommand commandSearchVCard = new SqlCommand("SELECT * FROM VCards WHERE phone_number = @phone_number", connection);
                commandSearchVCard.Parameters.AddWithValue("@phone_number", transaction.VCard);
                SqlDataReader readerVCard = commandSearchVCard.ExecuteReader();

                if (!readerVCard.Read())
                {
                    readerVCard.Close();
                    connection.Close();
                    return Content((HttpStatusCode)422, "VCard doesn't exist");
                }
                
                decimal earning_percentage = readerVCard.GetDecimal(3) > 0 ? readerVCard.GetDecimal(3)/100 : 0;
                decimal balance = readerVCard.GetDecimal(1);
                decimal max_debit = readerVCard.GetDecimal(2);
                int user_id = (int)readerVCard["user_id"];

                SqlCommand commandSearchUser = new SqlCommand("SELECT * FROM Users WHERE id = @id", connection);
                commandSearchUser.Parameters.AddWithValue("@id", (int)readerVCard["user_id"]);
                readerVCard.Close();
                SqlDataReader readerUser = commandSearchUser.ExecuteReader();

                if (!readerUser.Read())
                {
                    throw new Exception();
                }

                string hashedReceivedConfirmationCode;

                using (SHA256 mySHA256 = SHA256.Create())
                {
                    hashedReceivedConfirmationCode = Convert.ToBase64String(mySHA256.ComputeHash(Encoding.UTF8.GetBytes(confirmation_code)));
                }

                if (hashedReceivedConfirmationCode != (string)readerUser["confirmation_code"]){
                    readerUser.Close();
                    connection.Close();
                    return Content((HttpStatusCode)422, "Invalid confirmation code");
                }
                
                readerUser.Close();
                
                if (balance < transaction.Value)
                {
                    connection.Close();
                    return Content((HttpStatusCode)422, "Insufficient funds");
                } else if (max_debit < transaction.Value)
                {
                    connection.Close();
                    return Content((HttpStatusCode)422, "Value greater than the max debit of the selected vcard");
                }

                if (transaction.Category_id != 0)
                {
                    SqlCommand commandSearchCategory = new SqlCommand("SELECT * FROM Categories WHERE id = @id AND user_id = @user_id", connection);
                    commandSearchCategory.Parameters.AddWithValue("@id", transaction.Category_id);
                    commandSearchCategory.Parameters.AddWithValue("@user_id", user_id);
                    SqlDataReader readerCategory = commandSearchCategory.ExecuteReader();

                    if (!readerCategory.Read())
                    {
                        readerCategory.Close();
                        readerUser.Close();
                        connection.Close();
                        return Content((HttpStatusCode)422, "Invalid category");
                    }

                    readerCategory.Close();
                }
                
                sqlTransaction = connection.BeginTransaction();
                
                string sql = "INSERT INTO VCardTransactions VALUES (@vcard, @date, @type, @value, @category_id, @description)";
                SqlCommand commandInsertTransaction = new SqlCommand(sql, connection, sqlTransaction);

                commandInsertTransaction.Parameters.AddWithValue("@vcard", transaction.VCard);
                commandInsertTransaction.Parameters.AddWithValue("@date", DateTime.Now);
                commandInsertTransaction.Parameters.AddWithValue("@type", transaction.Type);
                commandInsertTransaction.Parameters.AddWithValue("@value", transaction.Value);
                
                if (transaction.Category_id != 0)
                {
                    commandInsertTransaction.Parameters.AddWithValue("@category_id", transaction.Category_id);
                } else
                {
                    commandInsertTransaction.Parameters.AddWithValue("@category_id", DBNull.Value);
                }

                commandInsertTransaction.Parameters.AddWithValue("@description", transaction.Description);

                int numTransactionsCreated = commandInsertTransaction.ExecuteNonQuery();

                string operation = "+";

                if (transaction.Type == 'D')
                {
                    operation = "-";
                    transaction.Value -= transaction.Value*earning_percentage;
                }

                sql = $"UPDATE VCards SET balance = balance {operation} @value WHERE phone_number = @phone_number";
                SqlCommand commandUpdateBalance = new SqlCommand(sql, connection, sqlTransaction);

                commandUpdateBalance.Parameters.AddWithValue("@operation", operation);
                commandUpdateBalance.Parameters.AddWithValue("@value", transaction.Value);
                commandUpdateBalance.Parameters.AddWithValue("@phone_number", transaction.VCard);

                int numVCardsUpdated = commandUpdateBalance.ExecuteNonQuery();
                
                sqlTransaction.Commit();

                sqlTransaction.Dispose();
                connection.Close();

                if (numTransactionsCreated > 0 && numVCardsUpdated > 0)
                {
                    return Ok();
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception e)
            {
                if (sqlTransaction != null)
                {
                    sqlTransaction.Rollback();
                    sqlTransaction.Dispose();
                }
                
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }

                return InternalServerError(e);
            }
        }
    }
}
