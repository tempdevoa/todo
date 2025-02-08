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

    [HttpGet(Name = "GetAllTodos")]
    public IEnumerable<TodoItem> Get()
    {
        return todos;
    }

    [HttpPost(Name = "AddTodo")]
    public TodoItem Add(TodoItem newTodoTask)
    {
        todos.Add(newTodoTask);
        return newTodoTask;
    }

    [HttpPost("{id}", Name = "CompleteTodo")]
    public void Complete(string id)
    {
        var foundTodo = todos.FirstOrDefault(p => p.Id.Equals(id));
        foundTodo?.Complete();
    }

    [HttpDelete("{id}", Name = "DeleteTodo")]
    public void Delete(string id)
    {
        var foundTodo = todos.FirstOrDefault(p => p.Id.Equals(id));
        if(foundTodo != null)
        {
            todos.Remove(foundTodo);
        }
    }
}
