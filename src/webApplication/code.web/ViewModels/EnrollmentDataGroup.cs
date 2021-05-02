using System.ComponentModel.DataAnnotations;
using System;

namespace code.web.ViewModels
{
    public class EnrollmentDataGroup
    {
        [DataType(DataType.Date)]
        public DateTime? EnrollmentDate { get; set; }

        public int StudentCount { get; set; }
    }
}