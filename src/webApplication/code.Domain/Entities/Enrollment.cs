using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace code.Domain.Entities
{
    public enum Grade
    {
        Pass,Fail,Drop
    }
    public class Enrollment
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public int StudentId { get; set; }

        [DisplayFormat(NullDisplayText="No grade")]

        private int _grade{ get; set; }
        public Grade? Grade { get; private set; }
        [JsonIgnore]
        [IgnoreDataMember] 
        public Course Course{ get; private set; }
        [JsonIgnore]
        [IgnoreDataMember] 
        public Student Student { get; private set; }

        public Enrollment(int studentId,int courseId,Grade? grade=null)
        {
            StudentId = studentId;
            CourseId = courseId;
            Grade = grade;
        }

        public static Enrollment CreateNew(int studentId,int courseId,Grade? grade=null)
        {
            return new Enrollment(studentId, courseId, grade);
        }
    }
}