using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace code.Api.Extensions.Pagination
{
    public static class PagerExtension
    {
        public static async Task<Pager<TResult>> PaginationAsync<TResult>(this IQueryable<TResult> query, int page,int limit,CancellationToken cancellationToken)
        where TResult:class
        {
            var paged = new Pager<TResult>();
            page= ( page < 0 ) ? 1 : page;
            
            paged.CurrentPage = page;
            paged.PageSize = limit;
            // var totalItemsCountTask = query.CountAsync(cancellationToken);

            var startRow = (page - 1) * limit;
            paged.Items = await query.Skip(startRow).Take(limit).ToListAsync(cancellationToken);

            paged.TotalItems = await query.CountAsync(cancellationToken);
            paged.TotalPages = (int)Math.Ceiling(paged.TotalItems / (double)limit);
            return paged;
        }
    }
}