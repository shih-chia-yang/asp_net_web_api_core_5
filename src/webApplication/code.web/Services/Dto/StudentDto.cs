using System;
using System.ComponentModel.DataAnnotations;

namespace code.web.Services.Dto
{
    public class StudentDto
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime EnrollmentDate{ get; set; }

        public StudentDto()
        {
            
        }
        public StudentDto(string lastName,string firstName,DateTime enrollmentDate)
        {
            LastName = lastName;
            FirstName = firstName;
            EnrollmentDate = enrollmentDate;
        }
    }
}