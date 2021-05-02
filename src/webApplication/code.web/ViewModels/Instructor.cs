using System.Collections.Generic;
using System;

namespace code.web.ViewModels
{
    public record Instructor
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime HireDate{ get; set; }

        public IEnumerable<Catalog> Class { get; set; }
    }
}