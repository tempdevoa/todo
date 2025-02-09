using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await dbContext.TodoItems.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(TodoItem newTodoTask)
    {
        await dbContext.TodoItems.AddAsync(newTodoTask);
        await dbContext.SaveChangesAsync();

        return Created(nameof(GetAsync), newTodoTask);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, TodoItem updatedTodoItem)
    {
        var foundTodo = await dbContext.TodoItems.FindAsync(updatedTodoItem.Id);
        if(foundTodo != null)
        {
            foundTodo.Adopt(updatedTodoItem);
            dbContext.Update(foundTodo);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
        else
        {
            return NotFound();
        }            
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        var foundTodo = await dbContext.TodoItems.FindAsync(id);
        if (foundTodo != null)
        {
            dbContext.TodoItems.Remove(foundTodo);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }
}
