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
    public IActionResult Get()
    {
        return Ok(dbContext.TodoItems.ToList());
    }

    [HttpPost(Name = "CreateTodoItem")]
    public IActionResult Add([FromBody] TodoItem newTodoTask)
    {
        dbContext.TodoItems.Add(newTodoTask);
        dbContext.SaveChanges();

        return Created(nameof(Get), newTodoTask);
    }

    [HttpPut("{id}", Name = "UpdateTodoItem")]
    public IActionResult Update(string id, [FromBody] TodoItem updatedTodoItem)
    {
        var foundTodo = dbContext.TodoItems.FirstOrDefault(p => p.Id.Equals(id));
        if(foundTodo != null)
        {
            foundTodo.Adopt(updatedTodoItem);
            dbContext.Update(foundTodo);
            dbContext.SaveChanges();

            return Ok();
        }
        else
        {
            return NotFound();
        }            
    }

    [HttpDelete("{id}", Name = "DeleteTodoItem")]
    public IActionResult Delete(string id)
    {
        var foundTodo = dbContext.TodoItems.FirstOrDefault(p => p.Id.Equals(id));
        if(foundTodo != null)
        {
            dbContext.TodoItems.Remove(foundTodo);
            dbContext.SaveChanges();

            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }
}
