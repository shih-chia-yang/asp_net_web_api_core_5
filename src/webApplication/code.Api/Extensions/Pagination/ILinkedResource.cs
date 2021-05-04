using System.Collections.Generic;
namespace code.Api.Extensions.Pagination
{
    public interface ILinkedResource
    {
        IDictionary<LinkedResourceType,LinkedResource> Links{ get; set; }
    }
}