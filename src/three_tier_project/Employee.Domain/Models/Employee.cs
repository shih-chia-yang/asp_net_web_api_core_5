namespace Employee.Domain.Models
{
    public class EmployeeModel : IBaseEntity
    {
        public int Id { get; set; }

        public string  Name { get; set; }   

        public string Gender { get; set; }

        public int? Salary { get; set; }

        public int? DepartmentId { get; set; }

        public static EmployeeModel Create(string name,string gender,int salary,int departmentId)
        {
            return new EmployeeModel() { Name = name, Gender = gender, Salary = salary, DepartmentId = departmentId };
        }
    }
}