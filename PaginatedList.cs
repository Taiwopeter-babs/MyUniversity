using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace MyUniversity;

public class PaginatedList<T> : List<T>
{
    public int PageIndex { get; set; }
    public int TotalPages { get; private set; }

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        // count is the total number of rows in the table
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        this.AddRange(items);
    }

    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public static async Task<PaginatedList<T>> CreateAsync(
        IQueryable<T> source, int pageIndex, int pageSize
    )
    {
        // this count should be cached
        var count = await source.CountAsync();
        var items = await source.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();
        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }
}