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
    public static class InOutStockService
    {
        public static async Task<bool> InStock(PutInOutModel model)
        {
            try
            {
                var httpClient = new HttpClient();
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                string url = Constants.API_URL + "InStock/Post";
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task<List<Unit>> GetUnit(string department)
        {
            try
            {
                string url = Constants.API_URL + $"InStock/GetUnits?department={department}";
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<Unit>>(response);
            }
            catch (Exception ex)
            {
                return new List<Unit>();
            }
        }

        public static async Task<List<Unit>> GetUnitBySparepare(string department, string code)
        {
            try
            {
                string url = Constants.API_URL + $"InStock/GetUnitBySparepart?department={department}&code={code}";
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<Unit>>(response);
            }
            catch (Exception ex)
            {
                return new List<Unit>();
            }
        }

        public static async Task<List<PartInfo>> GetNameBySparepare(string department, string code)
        {
            try
            {
                string url = Constants.API_URL + $"InStock/GetNameBySparepart?department={department}&code={code}";
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<PartInfo>>(response);
            }
            catch (Exception ex)
            {
                return new List<PartInfo>();
            }
        }

        public static async Task<List<string>> GetSparepartByEncript(string department, string code)
        {
            try
            {
                string url = Constants.API_URL + $"InStock/GetSparepartByEncript?department={department}&code={code}";
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<string>>(response);
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        public static async Task<List<PutInModel>> GetPutInHistory(string department)
        {
            try
            {
                string url = Constants.API_URL + $"InStock/GetPutInHistory?department={department}";
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<PutInModel>>(response);
            }
            catch (Exception ex)
            {
                return new List<PutInModel>();
            }
        }

        public static async Task<List<PutOutModel>> GetPutOutHistory(string department)
        {
            try
            {
                string url = Constants.API_URL + $"InStock/GetPutOutHistory?department={department}";
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<PutOutModel>>(response);
            }
            catch (Exception ex)
            {
                return new List<PutOutModel>();
            }
        }

        public static async Task<string> GetGasInforInSparepart(string department, string gasId)
        {
            try
            {
                string url = Constants.API_URL + $"InStock/GetGasInforInSparepart?department={department}&gasId={gasId}";
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.GetStringAsync(url);
                return response;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static async Task<List<ReturnGasModel>> GetGasInforReturn(string department)
        {
            try
            {
                string url = Constants.API_URL + $"InStock/GetGasInforReturn?department={department}";
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<ReturnGasModel>>(response);
            }
            catch (Exception ex)
            {
                return new List<ReturnGasModel>();
            }
        }

        public static async Task<bool> PutGasInforReturn(ReturnGasModel model)
        {
            try
            {
                string url = Constants.API_URL + $"InStock/PutGasInforReturn";
                var httpClient = new HttpClient();
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task<List<InventoryGasModel>> GetGasInventory(string department)
        {
            try
            {
                string url = Constants.API_URL + $"InStock/GetGasInventory?department={department}";
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<InventoryGasModel>>(response);
            }
            catch (Exception ex)
            {
                return new List<InventoryGasModel>();
            }
        }
    }
}
