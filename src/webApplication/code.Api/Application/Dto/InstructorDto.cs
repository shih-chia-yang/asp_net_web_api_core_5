using System;
using System.Collections.Generic;
using code.Api.Application.Dto;

namespace code.Api.Application.Queries
{
    public record InstructorDto
    {
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime HireDate { get; set; }

        public List<CatalogDto> Class { get; set; }
    }
}