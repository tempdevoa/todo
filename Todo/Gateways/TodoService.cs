using Todo.Models;
using System.Net.Http.Json;

namespace Todo.Gateways
{
    public class TodoService : ITodoService
    {
        private readonly HttpClient _httpClient;

        public TodoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TodoTask>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TodoTask>>("api/todos");
        }

        public async Task AddAsync(TodoTask todo)
        {
            await _httpClient.PostAsJsonAsync("api/todos", todo);
        }

        public async Task CompleteAsync(TodoTask todo)
        {
            await _httpClient.PutAsJsonAsync($"api/todos/{todo.Id}", todo);
        }
    }
}
