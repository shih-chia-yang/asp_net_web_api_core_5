using Employee.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee.Infrastructure
{
    public class EmployeeContext:DbContext
    {
        public DbSet<EmployeeModel> Employees { get; set; }

        public EmployeeContext(DbContextOptions<EmployeeContext> options):base(options)
        {
            
        }
    }
}