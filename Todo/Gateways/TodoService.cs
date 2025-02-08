using Todo.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Net.Http.Headers;

namespace Todo.Gateways
{
    public class TodoService : ITodoService
    {
        private readonly HttpClient httpClient;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly Uri uri = new Uri(string.Format("http://192.168.199.1:5152/Todo"));

        public TodoService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
        }

        public async Task<List<TodoTask>> GetAllAsync()
        {
            var queriedTodoTasks = new List<TodoTask>();
                        
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    queriedTodoTasks = JsonSerializer.Deserialize<List<TodoTask>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return queriedTodoTasks ?? new List<TodoTask>();
        }

        public async Task AddAsync(TodoTask todo)
        {
            await httpClient.PostAsJsonAsync(uri, todo);
        }

        public async Task CompleteAsync(TodoTask todo)
        {
            await httpClient.PostAsync($"{uri}/{todo.Id}", null);
        }
    }
}
