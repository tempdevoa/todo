using Todo.Models;

namespace Todo.Gateways
{
    public interface ITodoItemService
    {
        Task<List<TodoItem>> GetAllAsync();

        Task<ResponseStatus> AddAsync(TodoItem todo);

        Task<ResponseStatus> UpdateAsync(TodoItem todo);

        Task<ResponseStatus> DeleteAsync(TodoItem todo);
    }
}
