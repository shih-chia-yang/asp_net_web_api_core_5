using System.Collections;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace code.Domain.Entities
{
    public class Student:Person
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}",ApplyFormatInEditMode=true)]
        [Display(Name="Enrolment Date")]
        public DateTime EnrollmentDate { get; set; }
        [JsonIgnore]
        public ICollection<Enrollment> Enrollments{ get; set; }

        public Student()
        {
            
        }
        public Student(string lastName,string firstname,DateTime enrollmentDate):this()
        {
            LastName = lastName;
            FirstName = firstname;
            EnrollmentDate = enrollmentDate;
        }

        public static Student CreateNew(string lastName,string firstname,DateTime enrollmentDate)
        {
            return new Student(lastName, firstname, enrollmentDate);
        }
    }
}