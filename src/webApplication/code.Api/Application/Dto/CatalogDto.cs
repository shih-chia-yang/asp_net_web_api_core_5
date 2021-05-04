using System;

namespace code.Api.Application.Dto
{
    public record CatalogDto
    {
        public int Id { get; set; }

        public CourseDto Course { get; set; }

        public string Name { get; set; }

        public decimal Tuition { get; set; }

        public DateTime StartDate { get; set; }
    }
}