using Microsoft.AspNetCore.Mvc;
using TodoAPI;
using TodoAPI.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers((options) =>
{
    options.Filters.Add<GlobalExceptionFilter>();
}).ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errorMessages = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToList() ?? new List<string>()
            );

        return new BadRequestObjectResult(new { Errors = errorMessages });
    };
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPostgreSqlDatabase(builder.Configuration);
builder.Services.AddScoped<ValidateTodoPayloadFilter>();
builder.Services.AddScoped<GlobalExceptionFilter>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
