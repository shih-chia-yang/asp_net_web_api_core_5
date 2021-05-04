using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using code.Api.Application.Dto;
using code.Api.Extensions;

namespace code.Api.Application.Queries
{
    public interface IStudentQueries
    {
        Task<StudentDto> FindAsync(int id);

        Task<StudentListDto> PaginationAsync(int limit, int page, CancellationToken cancellationToken,SortingParams param=null);
        Task<IEnumerable<StudentDto>> GetAllAsync(SortingParams param=null);

        // IQueryable<StudentDto> Sort(SortingParams param);
    }
}