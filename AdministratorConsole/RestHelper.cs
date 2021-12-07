using Newtonsoft.Json;
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
            try
            {
                var payload = "{\"OldPassword\": \"" + oldPassword + "\",\"NewPassword\": \"" + newPassword + "\"}";
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), BaseUrl + "api/admins/me");
                request.Content = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.SendAsync(request);
                string responseBody = await response.Content.ReadAsStringAsync();
                
                return response.StatusCode;
            }
            catch (HttpRequestException)
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public static async Task<HttpStatusCode> ChangeAdminEnabled(string email, string enabled)
        {
            try
            {
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), BaseUrl + "api/admins/" + email + "/enabled");
                request.Content = new StringContent(enabled, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.SendAsync(request);
                string responseBody = await response.Content.ReadAsStringAsync();
                
                return response.StatusCode;
            }
            catch (HttpRequestException)
            {
                return HttpStatusCode.InternalServerError;
            }
        }
        
        public static async Task<HttpStatusCode> DeleteExternalEntity(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(BaseUrl + "api/externalentities/" + id);
                string responseBody = await response.Content.ReadAsStringAsync();
                
                return response.StatusCode;
            }
            catch (HttpRequestException)
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public static async Task<(HttpStatusCode, List<Admin>)> GetAdmins()
        {
            HttpResponseMessage response = await client.GetAsync(BaseUrl + "api/admins");
            string responseBody = await response.Content.ReadAsStringAsync();

            List<Admin> admins = null;

            if (response.StatusCode == HttpStatusCode.OK)
                admins = JsonConvert.DeserializeObject<List<Admin>>(responseBody);

            return (response.StatusCode, admins);
        }

        public static async Task<(HttpStatusCode, List<ExternalEntity>)> GetExternalEntities()
        {
            HttpResponseMessage response = await client.GetAsync(BaseUrl + "api/externalentities");
            string responseBody = await response.Content.ReadAsStringAsync();

            List<ExternalEntity> externalEntities = null;

            if (response.StatusCode == HttpStatusCode.OK)
                externalEntities = JsonConvert.DeserializeObject<List<ExternalEntity>>(responseBody);

            return (response.StatusCode, externalEntities);
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
        
        public static async Task<(HttpStatusCode, Admin)> CreateAdmin(string name, string email, string password)
        {
            try	
            {
                var payload = "{\"Name\": \"" + name + "\",\"Email\": \"" + email + "\",\"Password\": \"" + password + "\"}";
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(BaseUrl + "api/admins", content);
                string responseBody = await response.Content.ReadAsStringAsync();

                Admin admin = null;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    admin = JsonConvert.DeserializeObject<Admin>(responseBody);
                }

                return (response.StatusCode, admin);
            }
            catch(HttpRequestException)
            {
                return (HttpStatusCode.InternalServerError, null);
            }
        }

        public static async Task<(HttpStatusCode, ExternalEntity)> CreateExternalEntity(string name, string endpoint)
        {
            try	
            {
                var payload = "{\"Name\": \"" + name + "\",\"Endpoint\": \"" + endpoint + "\"}";
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(BaseUrl + "api/externalentities", content);
                string responseBody = await response.Content.ReadAsStringAsync();

                ExternalEntity externalEntity = null;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    externalEntity = JsonConvert.DeserializeObject<ExternalEntity>(responseBody);
                }

                return (response.StatusCode, externalEntity);
            }
            catch(HttpRequestException)
            {
                return (HttpStatusCode.InternalServerError, null);
            }
        }
        public static async Task<(HttpStatusCode, List<UserExternalEntity>)> GetUsers()
        {
            HttpResponseMessage response = await client.GetAsync(BaseUrl + "api/users");
            string responseBody = await response.Content.ReadAsStringAsync();

            List<UserExternalEntity> users = null;

            if (response.StatusCode == HttpStatusCode.OK)
                users = JsonConvert.DeserializeObject<List<UserExternalEntity>>(responseBody);

            return (response.StatusCode, users);
        }
    }
}
