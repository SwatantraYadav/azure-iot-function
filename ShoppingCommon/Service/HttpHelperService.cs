using Newtonsoft.Json;
using Common.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common.Data;

namespace Common.Client
{
    public class HttpHelperService
    {
        private static object _syncLock = new object();
        private static HttpClient _httpClient;
        private static HttpHelperService _instance;
        public static HttpHelperService Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock(_syncLock)
                    {
                        if (_instance == null)
                            _instance = new HttpHelperService();
                    }    
                }

                return _instance;
            }
        }
        private HttpHelperService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> RegisterDeviceAsync(ManagedDevice device)
        {
            string jsonObject = JsonConvert.SerializeObject(device);
            var content = new StringContent(jsonObject, Encoding.UTF8);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await _httpClient.PostAsync(Constants.RegisterFunctionUri, content);
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }
    }
}
