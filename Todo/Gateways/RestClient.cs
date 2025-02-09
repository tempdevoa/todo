using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Todo.Gateways
{
    public class RestClient
    {
        private readonly HttpClient httpClient;
        private readonly Uri uri;

        public JsonSerializerOptions JsonSerializerOptions { get; }

        public RestClient(string restResource)
        {
            uri = new Uri($"http://192.168.199.1:5152/{restResource}");
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            JsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
        }

        public async Task<List<T>> GetAllAsync<T>()
        {
            var queriedTodoTasks = new List<T>();

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    queriedTodoTasks = JsonSerializer.Deserialize<List<T>>(content, JsonSerializerOptions);
                }
            }
            catch (Exception) { }

            return queriedTodoTasks ?? new List<T>();
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync<TValue>(TValue value)
        {
            return await httpClient.PostAsJsonAsync(uri, value);
        }

        public async Task DeleteAsync(object id)
        {
            await httpClient.DeleteAsync($"{uri}/{id}");
        }

        public async Task PutAsJsonAsync<TValue>(object id, TValue value)
        {
            try
            {
                await httpClient.PutAsJsonAsync($"{uri}/{id}", value);
            }
            catch (Exception) { }
        }
    }
}
