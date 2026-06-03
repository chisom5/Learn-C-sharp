namespace TodoAPI.Models;

public record Todos
{
    
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public bool IsCompleted { get; init; }
    public DateTime DueDate { get; init; }
    
}