using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using custom.webapi.Models;

namespace custom.webapi.Repositories
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly List<Employee> _context;
        public EmployeeRepository()
        {
            _context = Init();
        }

        public Employee FindById(int id)
        {
            return _context.Where(x=>x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context;
        }

        protected List<Employee> Init()
        {
            return new List<Employee>{
                Employee.Create(1,"john","male",1000,1,"sale"),
                Employee.Create(2,"stone","male",1200,2,"it"),
                Employee.Create(3,"amy","female",1100,1,"sale"),
                Employee.Create(4,"nacy","female",1000,1,"sale")
            };
        }
    }
}