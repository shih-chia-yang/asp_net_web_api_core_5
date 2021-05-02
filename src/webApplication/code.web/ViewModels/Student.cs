using System;
using System.Collections.Generic;

namespace code.web.ViewModels
{
    public record Student
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime EnrollmentDate{ get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }
}