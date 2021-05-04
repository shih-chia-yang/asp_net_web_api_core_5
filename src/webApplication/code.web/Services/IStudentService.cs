using System.Collections.Generic;
using System.Threading.Tasks;
using code.web.Services.Dto;
using code.web.ViewModels;

namespace code.web.Services
{
    public interface IStudentService
    {
        Task<StudentResponseViewModel> GetAllAsync(SortOrder sort=null,int limit=2,int page=1);

        Task<Student> FindAsync(string StudentId);

        Task<StudentDto> AddAsync(StudentDto student);

        Task<Student> UpdateAsync(Student student);

        Task DeleteAsync(int id);
    }
}