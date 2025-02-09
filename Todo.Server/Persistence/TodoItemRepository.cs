using Microsoft.EntityFrameworkCore;
using Todo.Server.Domain.TodoItemAggregate;

namespace Todo.Server.Persistence
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private IAppDbContext appDbContext;

        public TodoItemRepository(IAppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task AddAsync(TodoItem todoItem)
        {
            await appDbContext.TodoItems.AddAsync(todoItem);
        }

        public async Task<TodoItem?> FindAsync(object id)
        {
            return await appDbContext.TodoItems.FindAsync(id);
        }

        public async Task FlushAsync()
        {
            await appDbContext.SaveChangesAsync();
        }

        public void Remove(TodoItem todoItem)
        {
            appDbContext.TodoItems.Remove(todoItem);
        }

        public async Task<List<TodoItem>> ToListAsync()
        {
            return await appDbContext.TodoItems.ToListAsync();
        }

        public void Update(TodoItem todoItem)
        {
            appDbContext.TodoItems.Update(todoItem);
        }
    }
}
