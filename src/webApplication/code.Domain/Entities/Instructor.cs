using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace code.Domain.Entities
{
    public class Instructor:Person
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}",ApplyFormatInEditMode=true)]
        [Display(Name="Hire date")]
        public DateTime HireDate { get; set; }

        public ICollection<Catalog> Class { get; set; }

        public Instructor()
        {
            
        }

        public Instructor(string lastName,string firstName,DateTime hireDate)
        {
            LastName = lastName;
            FirstName = firstName;
            HireDate = hireDate;
        }

        public static Instructor CreateNew(string lastName,string firstName,DateTime hireDate)
        {
            return new Instructor(lastName,firstName,hireDate);
        }
    }
}