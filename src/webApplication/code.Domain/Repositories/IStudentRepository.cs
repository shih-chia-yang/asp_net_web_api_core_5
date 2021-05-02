using System.Collections.Generic;
using System.Threading.Tasks;
using code.Domain.Entities;
using code.Domain.Kernal;

namespace code.Domain.Repositories
{
    public interface IStudentRepository:IRepository<Student>
    {
        Task<IEnumerable<Student>> GetAllAsync();

        Task<Student> FindAsync(int id);

        Task<Student> Add(Student student);

        Student Update(Student student);

        Student Delete(Student student);
    }
}