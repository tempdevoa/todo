using Microsoft.AspNetCore.Mvc;
using Todo.Server.Domain.TodoItemAggregate;
using Todo.Server.Persistence;

namespace Todo.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoItemRepository todoItemRepository;
    
    public TodoController(ITodoItemRepository todoItemRepository)
    {
        this.todoItemRepository = todoItemRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await todoItemRepository.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(TodoItem? newTodoTask)
    {
        if(newTodoTask != null)
        {
            var alreadyExistingTodoItem = await todoItemRepository.FindAsync(newTodoTask.Id);
            if(alreadyExistingTodoItem == null)
            {
                await todoItemRepository.AddAsync(newTodoTask);
                await todoItemRepository.FlushAsync();

                return Created(nameof(GetAsync), newTodoTask);
            }
            else
            {
                return Conflict();
            }
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, TodoItem updatedTodoItem)
    {
        var foundTodo = await todoItemRepository.FindAsync(updatedTodoItem.Id);
        if(foundTodo != null)
        {
            foundTodo.Adopt(updatedTodoItem);
            todoItemRepository.Update(foundTodo);
            await todoItemRepository.FlushAsync();

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
        var foundTodo = await todoItemRepository.FindAsync(id);
        if (foundTodo != null)
        {
            todoItemRepository.Remove(foundTodo);
            await todoItemRepository.FlushAsync();

            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }
}
