using DevHabit.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DevHabit.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}