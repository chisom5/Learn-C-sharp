using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;
using TodoAPI.Data;
using TodoAPI.Filters;

namespace TodoAPI.Controllers;

[ApiController]
[Route("api/todos")]
public class TodosController : ControllerBase
{

    private readonly TodoDbContext _dbContext;

    public TodosController(TodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("all")] // GET: api/todos/all
    public async Task<ActionResult<IEnumerable<Todos>>> Todos()
    {
        return Ok(await _dbContext.Todos.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Todos>> TodosById(int id)
    {
        var todoItem = await _dbContext.Todos.SingleOrDefaultAsync(t => t.Id == id);
        if (todoItem is null)
        {
            return NotFound();
        }
        return Ok(todoItem);
    }

    [ServiceFilter(typeof(ValidateTodoPayloadFilter))]
    [HttpPost]
    public async Task<ActionResult> Todos([FromBody] Todos todo)
    {
        _dbContext.Todos.Add(todo);
        await _dbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(TodosById), new { id = todo.Id }, todo);
    }

    [ServiceFilter(typeof(ValidateTodoPayloadFilter))]
    [HttpPatch("{id}")]
    public async Task<ActionResult> Todos(int id, [FromBody] Todos todo)
    {
        var existingTodo = await _dbContext.Todos.SingleOrDefaultAsync(t => t.Id == id);
        if (existingTodo is null)
            return NotFound();

        existingTodo.Name = todo.Name;
        existingTodo.IsCompleted = todo.IsCompleted;
        existingTodo.DueDate = todo.DueDate;

        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Todos(int id)
    {
        var existingTodo = await _dbContext.Todos.SingleOrDefaultAsync(t => t.Id == id);
        if (existingTodo is null)
        {
            return NotFound();
        }

        _dbContext.Todos.Remove(existingTodo);
        await _dbContext.SaveChangesAsync();
        return NoContent();

    }
}
