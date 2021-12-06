using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VCardsMiddleware
{
    public class APIAUTHORIZATIONSERVERPROVIDER : OAuthAuthorizationServerProvider  
    {  
        string connectionString = Properties.Settings.Default.DBConnString;
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)  
        {  
            context.Validated(); //   
        }  
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)  
        {  
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            bool correctCredentials = false;
            bool enabled = false;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Admins WHERE email = @email", conn);
                command.Parameters.AddWithValue("@email", context.UserName);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    using (SHA256 mySHA256 = SHA256.Create())
                    {
                        string password = Convert.ToBase64String(mySHA256.ComputeHash(Encoding.UTF8.GetBytes(context.Password)));
                        if (password == (string)reader["password"])
                        {
                            correctCredentials = true;
                        } 
                    }
                    enabled = Char.Parse((string)reader["enabled"]) == '1' ? true : false;
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

            if (correctCredentials && enabled)  
            {  
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));  
                context.Validated(identity);  
            }  
            else  
            {  
                if (!correctCredentials)
                {
                    context.SetError("invalid_grant", "Provided username and password is incorrect");  
                } else
                {
                    context.SetError("admin_blocked", "Your admin account is not enabled");  
                }
                return;  
            }  
        }  
    }
}