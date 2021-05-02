using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using code.Api.Application.Queries;
using code.Domain.Repositories;
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
        public async Task<Student> FindAsync(int id)
        {
            var result = await GetQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);
            return result; 
        }

        public async Task<IEnumerable<Student>> GetAllAsync(SortingParams param=null)
        {
            var result = Enumerable.Empty<Student>();
            if(param !=null)
                result = await Sort(param);
            else
                result=await GetQueryable().ToListAsync();
            return result;
        }

        private IQueryable<Student> GetQueryable()
        {
            var query = _context.Students
            .AsQueryable()
                .Include(e => e.Enrollments)
                .ThenInclude(x => x.Course)
                .Select(x => new Student()
                {
                    Id = x.Id,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    EnrollmentDate = x.EnrollmentDate,
                    Enrollments=x.Enrollments.Select(e=>new Enrollment()
                    {
                        Id=e.Id,
                        StudentId=e.StudentId,
                        CourseId=e.CourseId,
                        Grade=e.Grade,
                        Course=new Course(){
                            Id=e.Course.Id,
                            Title=e.Course.Title,
                            Grade=e.Course.Grade
                    }}).ToList()
                }).AsNoTracking();
            return query;
        }

        public async Task<IEnumerable<Student>> Sort(SortingParams param)
        {
            try
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
                return await query.Provider.CreateQuery<Student>(methodCallExpression).ToListAsync();

            }
            catch(Exception ex)
            {
                return Enumerable.Empty<Student>();
            }
            
            // query=query.Provider.CreateQuery<Student>(methodCallExpression); 
        }
            
    }
}