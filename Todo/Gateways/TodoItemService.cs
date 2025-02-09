using Todo.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Net.Http.Headers;

namespace Todo.Gateways
{
    public class TodoItemService : ITodoItemService
    {
        private readonly HttpClient httpClient;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly Uri uri = new Uri(string.Format("http://192.168.199.1:5152/Todo"));

        public TodoItemService()
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

        public async Task<List<TodoItem>> GetAllAsync()
        {
            var queriedTodoTasks = new List<TodoItem>();

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    queriedTodoTasks = JsonSerializer.Deserialize<List<TodoItem>>(content, _serializerOptions);
                }
            }
            catch (Exception) { }

            return queriedTodoTasks ?? new List<TodoItem>();
        }

        public async Task AddAsync(TodoItem todo)
        {
            try
            {
                await httpClient.PostAsJsonAsync(uri, todo);
            }
            catch (Exception) { }
        }

        public async Task DeleteAsync(TodoItem todo)
        {
            try
            {
                await httpClient.DeleteAsync($"{uri}/{todo.Id}");
            }
            catch (Exception) { }
        }

        public async Task UpdateAsync(TodoItem todo)
        {
            try
            {
                await httpClient.PostAsJsonAsync($"{uri}/{todo.Id}", todo);
            }
            catch (Exception) { }
        }
    }
}