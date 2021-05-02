using System.Collections.Generic;
using System.Threading.Tasks;
using code.Domain.Entities;

namespace code.Api.Application.Queries
{
    public interface IInstructorQueries
    {
        Task<Instructor> FindAsync(int id);

        Task<IEnumerable<Instructor>> GetAllAsync();
    }
}