using Microsoft.AspNetCore.Mvc;
using Todo.Server.Domain;

namespace Todo.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private static readonly List<TodoTask> todos;

    static TodoController()
    {
        todos = new List<TodoTask>();
        todos.Add(new TodoTask("projekt_anlegen", "Projekt anlegen", true));
        todos.Add(new TodoTask("durchstich_implementieren", "Durchstich implementieren", false));
    }

    [HttpGet(Name = "GetAllTodos")]
    public IEnumerable<TodoTask> Get()
    {
        return todos;
    }

    [HttpPost(Name = "AddTodo")]
    public TodoTask Add(TodoTask newTodoTask)
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
}
