using Microsoft.AspNetCore.Mvc;
using Todo.Server.Domain.TodoItemAggregate;

namespace Todo.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private static readonly List<TodoItem> todos;

    static TodoController()
    {
        todos = new List<TodoItem>();
        todos.Add(new TodoItem("projekt_anlegen", "Projekt anlegen", true));
        todos.Add(new TodoItem("durchstich_implementieren", "Durchstich implementieren", false));
    }

    [HttpGet(Name = "GetAllTodoItems")]
    public IEnumerable<TodoItem> Get()
    {
        return todos;
    }

    [HttpPost(Name = "CreateTodoItem")]
    public TodoItem Add(TodoItem newTodoTask)
    {
        todos.Add(newTodoTask);
        return newTodoTask;
    }

    [HttpPost("{id}", Name = "UpdateTodoItem")]
    public void Update(string id, TodoItem updatedTodoItem)
    {
        var foundTodo = todos.FirstOrDefault(p => p.Id.Equals(id));
        foundTodo?.Adopt(updatedTodoItem);
    }

    [HttpDelete("{id}", Name = "DeleteTodoItem")]
    public void Delete(string id)
    {
        var foundTodo = todos.FirstOrDefault(p => p.Id.Equals(id));
        if(foundTodo != null)
        {
            todos.Remove(foundTodo);
        }
    }
}
