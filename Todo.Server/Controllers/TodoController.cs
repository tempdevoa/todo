using Microsoft.AspNetCore.Mvc;
using Todo.Server.Domain.TodoItemAggregate;
using Todo.Server.Persistence;

namespace Todo.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly AppDbContext dbContext;

    public TodoController(AppDbContext appDbContext)
    {
        dbContext = appDbContext;
    }

    [HttpGet(Name = "GetAllTodoItems")]
    public IEnumerable<TodoItem> Get()
    {
        return dbContext.TodoItems.ToList();
    }

    [HttpPut(Name = "CreateTodoItem")]
    public TodoItem Add([FromBody] TodoItem newTodoTask)
    {
        dbContext.TodoItems.Add(newTodoTask);
        dbContext.SaveChanges();

        return newTodoTask;
    }

    [HttpPost("{id}", Name = "UpdateTodoItem")]
    public void Update(string id, [FromBody] TodoItem updatedTodoItem)
    {
        var foundTodo = dbContext.TodoItems.FirstOrDefault(p => p.Id.Equals(id));
        if(foundTodo != null)
        {
            foundTodo.Adopt(updatedTodoItem);
            dbContext.Update(foundTodo);
            dbContext.SaveChanges();
        }
    }

    [HttpDelete("{id}", Name = "DeleteTodoItem")]
    public void Delete(string id)
    {
        var foundTodo = dbContext.TodoItems.FirstOrDefault(p => p.Id.Equals(id));
        if(foundTodo != null)
        {
            dbContext.TodoItems.Remove(foundTodo);
            dbContext.SaveChanges();
        }
    }
}
