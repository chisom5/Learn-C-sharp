using System.Linq.Dynamic.Core;
using System.Reflection;

namespace PolarisEcl.Application.Common;

public static class QueryableExtesnions
{
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, int pageNum, int pageSize)
    {
        return query.Skip((pageNum - 1) * pageSize).Take(pageSize);
    }

    public static IQueryable<T> ApplySort<T>(this IQueryable<T> query, string? sortBy) where T : class
    {
        if (string.IsNullOrWhiteSpace(sortBy))
            return query;

        var allowedProperties = typeof(T)
        .GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => p.Name)
        .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var sortingExpression = new List<string>();

        foreach (var item in sortBy.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            var tokens = item.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (tokens.Length == 0 || !allowedProperties.Contains(tokens[0]))
                continue;

            var direction = tokens.Length > 1 && tokens[1].Equals("desc", StringComparison.OrdinalIgnoreCase) ? "descending" : "ascending";

            sortingExpression.Add($"{tokens[0]} {direction}");
        }
        return sortingExpression.Count > 0 ? query.OrderBy(string.Join(", ", sortingExpression)) : query;
    }
}