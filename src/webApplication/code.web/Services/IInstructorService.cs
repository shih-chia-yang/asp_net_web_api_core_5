using System.Collections.Generic;
using System.Threading.Tasks;
using code.web.Services.Dto;
using code.web.ViewModels;

namespace code.web.Services
{
    public interface IInstructorService
    {
        Task<IEnumerable<Instructor>> GetAllAsync();

        Task<Instructor> FindAsync(int id);

        Task<InstructorDto> AddAsync(InstructorDto addnew);

        Task<Instructor> UpdateAsync(Instructor update);

        Task DeleteAsync(int id);
    }
}