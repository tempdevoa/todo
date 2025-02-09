using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Todo.Gateways
{
    public class RestClient
    {
        private const string AppServerIPandPort = "192.168.199.1:5152";

        private readonly string restResource;
        private readonly HttpClient httpClient;
        private readonly Uri uri;

        public JsonSerializerOptions JsonSerializerOptions { get; }

        public RestClient(string restResource)
        {
            this.restResource = restResource;
            uri = new Uri($"http://{AppServerIPandPort}/{this.restResource}");
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

        public async Task<ResponseStatus> PostAsJsonAsync<TValue>(TValue value)
        {
            var response = await httpClient.PostAsJsonAsync(uri, value);
            return ToResponseStatus(response.StatusCode);
        }

        public async Task<ResponseStatus> DeleteAsync(object id)
        {
            var response = await httpClient.DeleteAsync($"{uri}/{id}");
            return ToResponseStatus(response.StatusCode);
        }

        public async Task<ResponseStatus> PutAsJsonAsync<TValue>(object id, TValue value)
        {
            var response = await httpClient.PutAsJsonAsync($"{uri}/{id}", value);
            return ToResponseStatus(response.StatusCode);
        }

        private ResponseStatus ToResponseStatus(HttpStatusCode httpStatusCode)
        {
            switch (httpStatusCode)
            {
                case HttpStatusCode.OK:
                    return new ResponseStatus.Ok();
                case HttpStatusCode.Created:
                    return new ResponseStatus.Ok();
                case HttpStatusCode.NoContent:
                    return new ResponseStatus.Ok();
                case HttpStatusCode.Conflict:
                    return new ResponseStatus.Duplicate(restResource);
                case HttpStatusCode.NotFound:
                    return new ResponseStatus.NotFound(restResource);
            }

            return new ResponseStatus.Failed();
        }
    }
}
