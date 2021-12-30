using Newtonsoft.Json;
using SmartGas.Models;
using SmartGas.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SmartGas.ApiService
{
    public static class LoginApiService
    {
        public static async Task<bool> Login(string userId, string password,string department)
        {
            try
            {
                var loginModel = new LoginModel()
                {
                    Id = userId,
                    Name = userId,
                    Department = department,
                    Password = password
                };

                var httpClient = new HttpClient();
                var json = JsonConvert.SerializeObject(loginModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                string url = Constants.API_URL + "Accounts/Login";
                var response = await httpClient.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Token>(jsonResult);
                Preferences.Set("accessToken", result.access_token);
                Preferences.Set("userId", result.user_Id);
                Preferences.Set("userName", result.user_Name);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
