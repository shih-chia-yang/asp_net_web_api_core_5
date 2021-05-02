using System.Collections.Generic;
using System.Threading.Tasks;
using code.Domain.Entities;
using code.Domain.Kernal;

namespace code.Domain.Repositories
{
    public interface IInstructorRepository:IRepository<Instructor>
    {
        Task<IEnumerable<Instructor>> GetAllAsync();
        Task<Instructor> FindAsync(int id);
        Task<Instructor> AddAsync(Instructor instructor);

        Instructor Delete(Instructor instructor);

        Instructor Update(Instructor instructor);
    }
}