using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;

namespace TodoAPI;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPostgreSqlDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetSection("PostgreSqlDatabase:ConnectionString").Value;

        services.AddDbContext<TodoDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}
