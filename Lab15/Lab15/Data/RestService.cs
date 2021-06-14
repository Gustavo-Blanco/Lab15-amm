using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Lab15.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xamarin.Forms;

namespace Lab15.Data
{
    public class RestService : IRestService
    {
        private HttpClient _client;
        
        public List<TodoItem> Items { get; private set; }


        public RestService()
        {
            #if DEBUG
                _client = new HttpClient(
                    DependencyService
                        .Get<IHttpClientHandlerService>()
                        .GetInsecureHandler()
                );
            #else
                _client = new HttpClient();
            #endif
        }

        public async Task<List<TodoItem>>  RefreshDataAsync()
        {
            Items = new List<TodoItem>();
            string action = "Get";

            var uri = new Uri(string.Format(Constants.RestURL, action));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"°\tError {0}",e.Message);
                throw;
            }

            return Items;
        }

        public async Task SaveTodoItemAsync(TodoItem item, bool isNewItem)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    var uri = new Uri(string.Format(Constants.RestURL, "Create"));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(Constants.RestURL, "Edit"));
                    response = await _client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully saved.");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"°\tError {0}",e.Message);
                throw;
            }
        }

        public async Task DeleteTodoItemAsync(string id)
        {
            var uri = new Uri(string.Format(Constants.RestURL, id));

            try
            {
                var response = await _client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully deleted. ");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"°\tError {0}",e.Message);
                throw;
            }
        }
    }
}