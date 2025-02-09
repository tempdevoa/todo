using Todo.Models;

namespace Todo.Gateways
{
    public interface ITodoItemService
    {
        Task<List<TodoItem>> GetAllAsync();

        Task AddAsync(TodoItem todo);

        Task UpdateAsync(TodoItem todo);

        Task DeleteAsync(TodoItem todo);
    }
}
