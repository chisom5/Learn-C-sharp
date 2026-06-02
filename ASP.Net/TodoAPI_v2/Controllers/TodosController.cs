using Microsoft.AspNetCore.Mvc;

namespace TodoAPI_v2.Controllers;

[ApiController]
[Route("api/todos")]
public class TodosController : ControllerBase
{

    private static readonly List<Todos> _todos = new List<Todos>();

    [HttpGet("all")] // GET: api/todos/all
    public async Task<ActionResult<IEnumerable<Todos>>> Todos()
    {
        return Ok(_todos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Todos>> TodosById(int id)
    {
        var todoItem = _todos.SingleOrDefault(t => t.Id == id);
        if (todoItem is null)
        {
            return NotFound();
        }
        return Ok(todoItem);
    }

    [HttpPost]
    public async Task<ActionResult> Todos([FromBody] Todos todo)
    {
        _todos.Add(todo);
        return CreatedAtAction(nameof(TodosById), new { id = todo.Id }, todo);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> Todos(int id, [FromBody] Todos todo)
    {
        var existingTodo = _todos.SingleOrDefault(t => t.Id == id);
        if (existingTodo is null)
        {
            return NotFound();
        }

        existingTodo = existingTodo with
        {
            Name = todo.Name,
            IsCompleted = todo.IsCompleted,
            DueDate = todo.DueDate
        };
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Todos (int id)
    {
        var existingTodo = _todos.SingleOrDefault(t => t.Id == id);
        if (existingTodo is null)
        {
            return NotFound();
        }

        _todos.Remove(existingTodo);
        return NoContent();
        
    }
}
