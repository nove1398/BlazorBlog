using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace APILib
{
    public class ApiHelper 
    {
        private HttpClient ApiClient { get; set; } = new HttpClient();



        public ApiHelper()
        {
           
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ApiHelper(string url)
        {
            
            ApiClient.BaseAddress = new Uri(url);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> Post<T>(string methodUrl, object model)
        {
            //HttpClient client = new HttpClient();

            // Now serialzize the object to json 
            string jsonData = JsonConvert.SerializeObject(model);

            // Create a content 
            HttpContent content = new StringContent(jsonData);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            content.Headers.Add("Access-Control-Allow-Credentials","true");


            // Make a request 
            var response = await ApiClient.PostAsync( methodUrl, content);
            var responseAsString = await response.Content.ReadAsStringAsync();

            // Deserialize the coming object into a T object 
            T obj = JsonConvert.DeserializeObject<T>(responseAsString);

            return obj;
        }

        public async Task<T> Delete<T>(string methodUrl)
        {

            var response = await ApiClient.DeleteAsync( methodUrl);
            var responseString = await response.Content.ReadAsStringAsync();

            T obj = JsonConvert.DeserializeObject<T>(responseString);

            return obj;
        }

        public async Task<T> Put<T>(string methodUrl, object model)
        {
            //HttpClient client = new HttpClient();

            // Serialize the object
            string jsonData = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(jsonData);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            content.Headers.Add("Access-Control-Allow-Credentials", "true");

            var response = await ApiClient.PutAsync(methodUrl, content);

            // Read the data 
            var responseString = await response.Content.ReadAsStringAsync();

            T obj = JsonConvert.DeserializeObject<T>(responseString);

            return obj;
        }

        public async Task<T> Get<T>(string methodUrl)
        {
            //HttpClient client = new HttpClient();

            // Send a request and get the response 
            var response = await ApiClient.GetAsync( methodUrl);

            // Read the data 
            var jsonData = await response.Content.ReadAsStringAsync();

            T obj = JsonConvert.DeserializeObject<T>(jsonData);

            return obj;
        }

        public async Task<List<T>> GetAll<T>(string methodUrl)
        {
            //HttpClient client = new HttpClient();

            var response = await ApiClient.GetAsync(methodUrl);
            // Send a request and get the response 

            // Read the data 
            var jsonData = await response.Content.ReadAsStringAsync();

            List<T> obj = JsonConvert.DeserializeObject<List<T>>(jsonData);

            return obj;
        }

    }
}
