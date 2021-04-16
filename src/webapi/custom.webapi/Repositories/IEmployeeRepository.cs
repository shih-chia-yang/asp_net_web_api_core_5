using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using custom.webapi.Models;

namespace custom.webapi.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();

        Employee FindById(int id);

        void Add(Employee employee);

        void Update(Employee employee);
    }
}