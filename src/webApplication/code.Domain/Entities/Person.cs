using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace code.Domain.Entities
{
    public class Person
    {
        
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name="Last Name")]
        public string  LastName { get; set; }

        [Required]
        [StringLength(50,ErrorMessage="First Name can not be longer than 50 characters")]
        [Column("FirstName")]
        [Display(Name="First Name")]
        public string  FirstName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{LastName},{FirstName}";
    }
}