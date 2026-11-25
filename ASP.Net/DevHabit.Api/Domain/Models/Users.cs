namespace DevHabit.Domain.Models;
public sealed class User
{
    public Guid Id {get; set;} = Guid.CreateVersion7();
    public string FirstName {get; set;} = string.Empty;
     public string LastName {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;

}