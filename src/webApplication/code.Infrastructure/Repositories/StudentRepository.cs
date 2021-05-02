using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using code.Domain.Entities;
using code.Domain.Kernal;
using code.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace code.Infrastructure.Repositories
{
    public class StudentRepository:IStudentRepository
    {
        private readonly DataContext _context;
        public StudentRepository(DataContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Student> Add(Student student)
        {
            await _context.AddAsync(student);
            return student;
        }

        public Student Delete(Student student)
        {
            _context.Remove(student);
            return student;
        }

        public async Task<Student> FindAsync(int id)
        {
            return await _context.Students
            .Where(x => x.Id == id)
            .Include(x=>x.Enrollments)
            .ThenInclude(e=>e.Course)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students
                            .AsNoTracking()
                            .ToListAsync();
        }

        public Student Update(Student student)
        {
            _context.Update(student);
            return student;
        }
    }
}