using System;

namespace code.web.ViewModels
{
    public record Catalog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Tuition { get; set; }
        public DateTime StartDate { get; set; }
        public Course Course { get; set; }
    }
}