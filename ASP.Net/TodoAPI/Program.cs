using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
var todos = new List<TodoItem>();

app.MapGet("/todos", () => todos); //get all todos

// get a single todo by id
app.MapGet("/todos/{id}", Results<Ok<TodoItem>, NotFound> (int id) =>
{
    var todoItem = todos.SingleOrDefault(t => t.Id == id);
    if (todoItem is null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(todoItem);
});

app.MapPost("/todo", (TodoItem todoItem) =>
{
    todos.Add(todoItem);

    return TypedResults.Created($"todo/{todoItem.Id}", todoItem);

}).AddEndpointFilter(async (context, next) =>
{
    var taskArgument = context.GetArgument<TodoItem>(0);
    var errors = new Dictionary<string, string[]>();
    if (taskArgument.DueDate < DateTime.UtcNow)
    {
        errors.Add(nameof(taskArgument.DueDate), ["Due date cannot be in the past."]);
    }
    if (taskArgument.IsCompleted && taskArgument.DueDate > DateTime.UtcNow)
    {
        errors.Add(nameof(taskArgument.DueDate), ["Cannot mark a task as completed if its due date is in the future."]);
    }
    if (taskArgument.IsCompleted)
    {
        errors.Add(nameof(taskArgument.IsCompleted), ["Cannot mark a task as completed."]);
    }

    if (errors.Count > 0)
    {
        return TypedResults.ValidationProblem(errors);
    }
    return await next(context);
});

app.MapPut("/todo/{id}", (int id, TodoItem updatedTodo) =>
{
    var todoItem = todos.FirstOrDefault(t => t.Id == id);
    if (todoItem is null)
    {
        return Results.NotFound();
    }

    todos.Remove(todoItem);
    todos.Add(updatedTodo);

    return TypedResults.Ok(todos);
});

app.MapDelete("/todo/{id}", (int id) =>
{
    // if deleted we don't return anything, just a status code.
    var todoItem = todos.RemoveAll(t => t.Id == id);
    return TypedResults.NoContent();

});

app.Run();
record TodoItem(int Id, string Name, DateTime DueDate, bool IsCompleted);