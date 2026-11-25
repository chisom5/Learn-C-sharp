using DevHabit.Application.Common.Interfaces;
using DevHabit.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace DevHabit.Data;

public sealed class AppDbContext(DbContextOptions options): DbContext(options), IAppDbContext
{
   public DbSet<User> Users {get; set;}
}



