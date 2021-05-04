using System.Collections.Generic;
namespace code.Api.Extensions.Pagination
{
    public class Pager<TResult>
    {
        const int MaxPageSize = 500;
        private int _pageSize;
        public int PageSize { get=>_pageSize; set =>_pageSize=(value > MaxPageSize) ?MaxPageSize:value; }

        public int CurrentPage { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public IList<TResult> Items { get; set; }

        public Pager()
        {
            Items = new List<TResult>();
        }

    }
}