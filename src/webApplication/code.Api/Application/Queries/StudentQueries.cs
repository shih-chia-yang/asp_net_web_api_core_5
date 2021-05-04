using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using code.Api.Application.Dto;
using code.Api.Extensions;
using code.Api.Extensions.Pagination;
using code.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace code.Api.Application.Queries
{
    public class StudentQueries : IStudentQueries
    {
        private readonly DataContext _context;
        public StudentQueries(DataContext context)
        {
            _context = context;
        }
        public async Task<StudentDto> FindAsync(int id)
        {
            var result = await GetQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);
            return result; 
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync(SortingParams param=null)
        {
            var result = Enumerable.Empty<StudentDto>();
            if(param !=null)
                result =await Sort(param).ToListAsync();
            else
                result=await GetQueryable().ToListAsync();
            return result;
        }

        private IQueryable<StudentDto> GetQueryable()
        {
            var query = _context.Students
            .AsQueryable()
                .Include(e => e.Enrollments)
                .ThenInclude(x => x.Course)
                .Select(x => new StudentDto()
                {
                    Id = x.Id,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    EnrollmentDate = x.EnrollmentDate,
                    Enrollments=x.Enrollments.Select(e=>new EnrollmentDto()
                    {
                        Id=e.Id,
                        StudentId=e.StudentId,
                        CourseId=e.CourseId,
                        Grade=e.Grade,
                        Course=new CourseDto(){
                            Id=e.Course.Id,
                            Title=e.Course.Title,
                            Grade=e.Course.Grade
                    }}).ToList()
                }).AsNoTracking();
            return query;
        }

        private IQueryable<StudentDto> Sort(SortingParams param)
        {
            var query = GetQueryable();
            ParameterExpression parameter = Expression.Parameter(query.ElementType, "x");  
            MemberExpression property = Expression.Property(parameter, param.SortBy);  
            LambdaExpression lambda = Expression.Lambda(property, parameter);
            string methodName = param.IsAscending ? "OrderBy" : "OrderByDescending";  
            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,  
                                new Type[] { query.ElementType, property.Type },  
                                query.Expression, Expression.Quote(lambda));
            var sqlString = query.ToQueryString();
            return query.Provider.CreateQuery<StudentDto>(methodCallExpression).AsNoTracking();
        }

        public async Task<StudentListDto> PaginationAsync(int limit, int page, CancellationToken cancellationToken,SortingParams param=null)
        {
            var students =(param == null) ?
            await GetQueryable().PaginationAsync(page, limit, cancellationToken) :
            await Sort(param).AsNoTracking().PaginationAsync(page, limit, cancellationToken);
            return new StudentListDto()
            {
                CurrentPage = students.CurrentPage,
                TotalPages = students.TotalPages,
                TotalItems = students.TotalItems,
                Items = students.Items
        };
        }
    }
}