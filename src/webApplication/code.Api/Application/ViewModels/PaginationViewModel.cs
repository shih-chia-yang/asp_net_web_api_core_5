using System.Collections.Generic;
using code.Api.Extensions.Pagination;

namespace code.Api.Application.ViewModels
{
    public class PaginationViewModel<TDto> : ILinkedResource
    {
        public int CurrentPage { get; set; } 

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public IList<TDto> Items { get; set; }
        public IDictionary<LinkedResourceType, LinkedResource> Links { get; set; }
    }
}