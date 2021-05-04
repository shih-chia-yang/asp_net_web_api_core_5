using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using code.Api.Application.Dto;
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
        public async Task<InstructorDto> FindAsync(int id)
        {
            var result = await _context.Instructors
            .Where(x => x.Id==id).AsNoTracking()
            .Include(ca=>ca.Class)
            .ThenInclude(c=>c.Course)
            .Select(x=>new InstructorDto()
            {
                Id=x.Id,
                LastName=x.LastName,
                FirstName=x.FirstName,
                HireDate=x.HireDate,
                Class=x.Class.Select(ca=>new CatalogDto()
                {
                    Id=ca.Id,
                    Name=ca.Name,
                    Tuition=ca.Tuition,
                    StartDate=ca.StartDate,
                    Course= new CourseDto()
                    {
                        Id=ca.Course.Id,
                        Title=ca.Course.Title,
                        Grade=ca.Course.Grade
                    }
                }).ToList()
            }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<InstructorDto>> GetAllAsync()
        {
            var result =await _context.Instructors.AsNoTracking()
                    .Select(x=>new InstructorDto()
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