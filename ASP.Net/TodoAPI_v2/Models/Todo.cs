using System.ComponentModel.DataAnnotations;
namespace TodoAPI.Models;

public class Todos
{
    [Range(1, int.MaxValue, ErrorMessage = "Id must be a positive integer.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [MinLength(1, ErrorMessage = "Name cannot be empty.")]
    public string Name { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime DueDate { get; set; }

}