using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdministratorConsole
{
    class RestHelper
    {
        private static readonly string BaseUrl = "http://localhost:50148/";

        private static string token = "";

        static readonly HttpClient client = new HttpClient();

        public static void Logout()
        {
            token = "";
        }

        public static async Task<HttpStatusCode> Login(string email, string password)
        {
            try	
            {
                var payload = "username=" + email + "&password=" + password + "&grant_type=password";
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = await client.PostAsync(BaseUrl + "token", content);
                string responseBody = await response.Content.ReadAsStringAsync();

                JObject o = JObject.Parse(responseBody);
                
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // get name token of first person and convert to a string
                    token = (string)o.SelectToken("access_token");
                }

                if (response.StatusCode == HttpStatusCode.BadRequest && (string)o.SelectToken("error") == "admin_blocked")
                {
                    return HttpStatusCode.Unauthorized;
                }

                return response.StatusCode;
            }
            catch(HttpRequestException)
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}
