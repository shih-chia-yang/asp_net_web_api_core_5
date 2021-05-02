using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using code.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace code.Api.Application.Queries
{
    public class InstructorQueries : IInstructorQueries
    {
        private readonly DataContext _context;
        public InstructorQueries(DataContext context)
        {
            _context = context;
        }
        public async Task<Instructor> FindAsync(int id)
        {
            var result = await _context.Instructors
            .Where(x => x.Id==id).AsNoTracking()
            .Include(ca=>ca.Class)
            .ThenInclude(c=>c.Course)
            .Select(x=>new Instructor()
            {
                Id=x.Id,
                LastName=x.LastName,
                FirstName=x.FirstName,
                HireDate=x.HireDate,
                Class=x.Class.Select(ca=>new Catalog()
                {
                    Id=ca.Id,
                    Name=ca.Name,
                    Tuition=ca.Tuition,
                    StartDate=ca.StartDate,
                    Course= new Course()
                    {
                        Id=ca.Course.Id,
                        Title=ca.Course.Title,
                        Grade=ca.Course.Grade
                    }
                }).ToList()
            }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Instructor>> GetAllAsync()
        {
            var result =await _context.Instructors.AsNoTracking()
                    .Select(x=>new Instructor()
                    {
                        Id=x.Id,
                        LastName=x.LastName,
                        FirstName=x.FirstName,
                        HireDate=x.HireDate,
                    }).ToListAsync();
            return result;
        }
    }
}