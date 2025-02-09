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

        public async Task<ResponseStatus> AddAsync(TodoItem todo)
        {
            try
            {
                return await restClient.PostAsJsonAsync(todo);
            }
            catch (Exception)
            {
                return new ResponseStatus.Failed();
            }
        }

        public async Task<ResponseStatus> DeleteAsync(TodoItem todo)
        {
            try
            {
                return await restClient.DeleteAsync(todo.Id);
            }
            catch (Exception)
            {
                return new ResponseStatus.Failed();
            }
        }

        public async Task<ResponseStatus> UpdateAsync(TodoItem todo)
        {
            try
            {
                return await restClient.PutAsJsonAsync(todo.Id, todo);
            }
            catch (Exception)
            {
                return new ResponseStatus.Failed();
            }
        }
    }
}