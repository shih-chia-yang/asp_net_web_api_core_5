using System.Collections.Generic;
using System.Threading.Tasks;

namespace code.Api.Application.Queries
{
    public interface IInstructorQueries
    {
        Task<InstructorDto> FindAsync(int id);

        Task<IEnumerable<InstructorDto>> GetAllAsync();
    }
}