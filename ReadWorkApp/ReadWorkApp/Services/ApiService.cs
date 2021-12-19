using Newtonsoft.Json;
using ReadWorkApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ReadWorkApp.Services
{
    public static class ApiService
    {
        public static async Task<bool> RegisterUser(string name, string email, string password)
        {
            var registerModel = new RegisterModel()
            {
                Name = name,
                Email = email,
                Password = password
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(registerModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://vihicleapp.azurewebsites.net/api/accounts/register", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        public static async Task<bool> Login(string email, string password)
        {
            var loginModel = new LoginModel()
            {
                Email = email,
                Password = password
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(loginModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://vihicleapp.azurewebsites.net/api/accounts/login", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Token>(jsonResult);
            Preferences.Set("accessToken", result.access_token);
            return true;
        }

        public static async Task<bool> ChangePassword(string oldPassword,string newPassword,string confirmPass)
        {
            var changePassworModel = new ChangePasswordModel()
            {
                OldPassword = oldPassword,
                NewPassword = newPassword,
                ConfirmPassword = confirmPass
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(changePassworModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken",string.Empty));
            var response = await httpClient.PostAsync("https://vihicleapp.azurewebsites.net/api/accounts/ChangePassword", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public static async Task<bool> EditPhoneNumber(string number)
        {
            var httpClient = new HttpClient();
            var content = new StringContent($"Number={number}", Encoding.UTF8, "application/x-www-form-urlencoded");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.PostAsync("https://vihicleapp.azurewebsites.net/api/accounts/EditPhoneNumber", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public static async Task<bool> EditUserProfile(byte[] imageArray)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(imageArray);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.PostAsync("https://vihicleapp.azurewebsites.net/api/accounts/EditUserProfile", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public static async Task<UserImageModel> UserProfileImage()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync("https://vihicleapp.azurewebsites.net/api/accounts/UserProfileImage");
          
            return JsonConvert.DeserializeObject<UserImageModel>(response);
        }

        public static async Task<List<Category>> GetCategory()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync("https://vihicleapp.azurewebsites.net/api/Categories");

            return JsonConvert.DeserializeObject<List<Category>>(response);
        }

        public static async Task<bool> Images(int vehicleId,byte[] imageArray)
        {
            var VehicleImage = new VehicleImage()
            {
                VehicleId = vehicleId,
                ImageArray = imageArray
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(VehicleImage);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.PostAsync("https://vihicleapp.azurewebsites.net/api/Images", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public static async Task<VehicleDetail> GetVehicleDetail(int id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync($"https://vihicleapp.azurewebsites.net/api/Vehicles/VehicleDetails?id={id}");

            return JsonConvert.DeserializeObject<VehicleDetail>(response);
        }

        public static async Task<List<VehicleByCategory>> GetVehicleByCategory(int id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync($"https://vihicleapp.azurewebsites.net/api/Vehicles?categoryId={id}");

            return JsonConvert.DeserializeObject<List<VehicleByCategory>>(response);
        }

        public static async Task<List<SearchVehicle>> SearchVehicles(string search)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync($"https://vihicleapp.azurewebsites.net/api/Vehicles/SearchVehicles?search={search}");
            return JsonConvert.DeserializeObject<List<SearchVehicle>>(response);
        }

        public static async Task<VehicleResponse> AddVehicle(AddVehicle vehicle)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(vehicle);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.PostAsync("https://vihicleapp.azurewebsites.net/api/Vehicles", content);
            var jsonResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<VehicleResponse>(jsonResult);
        }

        public static async Task<List<HotAndNewAd>> HotAndNewAds()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync($"https://vihicleapp.azurewebsites.net/api/Vehicles/HotAndNewAds");
            return JsonConvert.DeserializeObject<List<HotAndNewAd>>(response);
        }

        public static async Task<List<MyAds>> MyAds()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync($"https://vihicleapp.azurewebsites.net/api/Vehicles/MyAds");
            return JsonConvert.DeserializeObject<List<MyAds>>(response);
        }
    }
}
