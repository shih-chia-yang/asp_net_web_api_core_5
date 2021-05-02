using System;
using System.Collections.Generic;

namespace code.Api.Application.Queries
{
    public record Instructor
    {
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime HireDate { get; set; }

        public List<Catalog> Class { get; set; }
    }

    public record Catalog
    {
        public int Id { get; set; }

        public Course Course { get; set; }

        public string Name { get; set; }

        public decimal Tuition { get; set; }

        public DateTime StartDate { get; set; }
    }
}