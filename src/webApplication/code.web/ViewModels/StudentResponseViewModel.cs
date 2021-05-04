using System.Collections.Generic;

namespace code.web.ViewModels
{
    public class StudentResponseViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public List<Student> Items {get;set;}

        public Student Default=>new Student();
        
        public IDictionary<string,Linked> Links { get; set;}
    }

    public class Linked
    {
        public string  Href { get; set; }
    }
}