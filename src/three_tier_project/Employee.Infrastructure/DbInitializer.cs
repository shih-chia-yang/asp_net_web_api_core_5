using System.Linq;
using Employee.Domain.Models;

namespace Employee.Infrastructure
{
    public class DbInitializer
    {
        public static void Initializer (EmployeeContext context)
        {
            context.Database.EnsureCreated();
            if(context.Employees.Any())
                return;
            var employeeList = new EmployeeModel[]{

                EmployeeModel.Create("mike","male",8000,1),
                EmployeeModel.Create("adam","male",5000,1),
                EmployeeModel.Create("jacky","female",9000,1)
            };

            foreach(var e in employeeList)
            {
                context.Employees.Add(e);
            }
            context.SaveChanges();
        }
    }
}