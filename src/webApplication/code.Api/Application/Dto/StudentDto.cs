using System;
using System.Collections.Generic;
using code.Api.Extensions.Pagination;
using Microsoft.AspNetCore.Mvc.Routing;

namespace code.Api.Application.Dto
{
    public record StudentDto
    {
        public int Id { get; init; }
        public string LastName { get; init; }
        public string FirstName { get; init; }
        public DateTime EnrollmentDate{ get; init; }
        public List<EnrollmentDto> Enrollments { get; init; }
    }

    public record StudentListDto:ILinkedResource
    {
        public int CurrentPage { get; init; }

        public int TotalItems { get; init; }

        public int TotalPages { get; init; }

        public IList<StudentDto> Items { get; init; }

        public IDictionary<LinkedResourceType, Extensions.Pagination.LinkedResource> Links { get; set; }

    }
}