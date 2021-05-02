using System;

namespace code.web.Services.Dto
{
    public class InstructorDto
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime HireDate { get; set; }

        public InstructorDto()
        {
            
        }

        public InstructorDto(string lastName,string firstName,DateTime hireDate)
        {
            LastName = lastName;
            FirstName = firstName;
            HireDate = hireDate;
        }
    }
}