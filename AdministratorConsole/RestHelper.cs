using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AdministratorConsole
{
    class RestHelper
    {
        private static readonly string BaseUrl = "http://localhost:50148/";

        static readonly HttpClient client = new HttpClient();

        public static void Logout()
        {
            client.DefaultRequestHeaders.Authorization = null;
        }

        public static async Task<HttpStatusCode> ChangePassword(string oldPassword, string newPassword)
        {
            var payload = "{\"OldPassword\": " + oldPassword + ",\"NewPassword\": " + newPassword + "}";
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), BaseUrl + "api/admins/me");
            request.Content = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            string responseBody = await response.Content.ReadAsStringAsync();
            
            return response.StatusCode;
        }

        public static async Task<(HttpStatusCode, string, string)> GetAdminInfo()
        {
            try	
            {
                HttpResponseMessage response = await client.GetAsync(BaseUrl + "api/admins/me");
                string responseBody = await response.Content.ReadAsStringAsync();

                JObject o = JObject.Parse(responseBody);

                return (response.StatusCode, (string)o.SelectToken("Name"), (string)o.SelectToken("Email"));
            }
            catch(HttpRequestException)
            {
                return (HttpStatusCode.InternalServerError, null, null);
            }
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
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)o.SelectToken("access_token"));
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
