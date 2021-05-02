using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using code.Domain.Entities;

namespace code.Api.Application.Queries
{
    public record Student
    {
        public int Id { get; init; }
        public string LastName { get; init; }
        public string FirstName { get; init; }
        public DateTime EnrollmentDate{ get; init; }
        public List<Enrollment> Enrollments { get; init; }
    }

    public record Enrollment
    {
        public int Id { get; init; }

        public int CourseId { get; init; }

        public int StudentId { get; init; }
        public Grade? Grade { get; init; }

        public Course Course{ get; init; }
    }

    public record Course{

        public int Id { get; init; }
        public string Title { get; init; }
        public int Grade { get; init; }  
    }
}