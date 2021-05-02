
namespace code.web.ViewModels
{
    public enum Grade{
        Pass,Fail,Drop
    }
    public record Enrollment
    {
        public int Id { get; init; }

        public int CourseId { get; init; }

        public int StudentId { get; init; }
        public Grade? Grade { get; init; }

        public Course Course{ get; init; }
    }
}