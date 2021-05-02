using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace code.Domain.Entities
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name="Number")]
        public int Id { get; set; }

        [StringLength(50,MinimumLength=3)]
        public string Title { get; set; }

        [Range(0,3)]
        public int Grade { get; set; }

        [JsonIgnore]
        public ICollection<Enrollment> Enrollments { get; set; }

        public Course()
        {
            
        }

        public Course(int id, string title, int grade) => (Id, Title, Grade) = (id, title, grade);

        public static Course CreateNew(int id, string title, int grade) => new Course(id, title, grade);
    }
}