namespace custom.webapi.Models
{
    public class Employee
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Salary { get; set; }
        public int DepartmentId { get; set; }

        public string Department { get; set; }

        public Employee(int id, string name, string gender, int salary, int departmentId, string department)
        {
            this.Id = id;
            this.Name = name;
            this.Gender = gender;
            this.Salary = salary;
            this.DepartmentId = departmentId;
            this.Department = department;

        }
        
        public static Employee Create(int id,string name, string gender, int salary, int departmentId, string department)
        {
            return new Employee(id,name, gender, salary, departmentId, department);
        }
    }
}