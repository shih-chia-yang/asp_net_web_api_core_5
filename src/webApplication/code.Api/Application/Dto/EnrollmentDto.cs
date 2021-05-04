using code.Domain.Entities;

namespace code.Api.Application.Dto
{
    public record EnrollmentDto
    {
        public int Id { get; init; }

        public int CourseId { get; init; }

        public int StudentId { get; init; }
        public Grade? Grade { get; init; }

        public CourseDto Course{ get; init; }
    }
}