using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using code.Domain.Entities;
using code.Domain.Kernal;
using code.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace code.Infrastructure.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly DataContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public InstructorRepository(DataContext context)
        {
            _context=context;
        }
        public async Task<Instructor> FindAsync(int id)
        {
            return await _context.Instructors
                .Where(x=>x.Id==id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Instructor>> GetAllAsync()
        {
            return await _context.Instructors.AsNoTracking().ToListAsync();
        }

        public async Task<Instructor> AddAsync(Instructor instructor)
        {
            await _context.AddAsync(instructor);
            return instructor;
        }

        public Instructor Delete(Instructor instructor)
        {
            _context.Remove(instructor);
            return instructor;
        }

        public Instructor Update(Instructor instructor)
        {
            _context.Update(instructor);
            return instructor;
        }
    }
}