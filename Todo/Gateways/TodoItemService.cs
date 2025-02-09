using Todo.Models;

namespace Todo.Gateways
{
    public class TodoItemService : ITodoItemService
    {
        private readonly RestClient restClient;

        public TodoItemService()
        {
            restClient = new RestClient("Todo");
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {

            return await restClient.GetAllAsync<TodoItem>();
        }

        public async Task AddAsync(TodoItem todo)
        {
            try
            {
                await restClient.PostAsJsonAsync(todo);
            }
            catch (Exception) { }
        }

        public async Task DeleteAsync(TodoItem todo)
        {
            try
            {
                await restClient.DeleteAsync(todo.Id);
            }
            catch (Exception) { }
        }

        public async Task UpdateAsync(TodoItem todo)
        {
            try
            {
                await restClient.PutAsJsonAsync(todo.Id, todo);
            }
            catch (Exception) { }
        }
    }
}