using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using code.Domain.Entities;

namespace code.Infrastructure
{
    public static class DataContextSeed
    {
        public static async Task SeedAsync(DataContext context)
        {
            var useCustomizationData = true;
            string contentPath = Environment.CurrentDirectory;
            try
            {
                if(!context.Students.Any()&& useCustomizationData)
                {
                    await context.Students.AddRangeAsync(GetPreConfigurationStudents());
                    context.SaveChanges();
                }
                if (!context.Courses.Any()&& useCustomizationData)
                {
                    await context.Courses.AddRangeAsync(GetPreConfigurationCourses());
                    context.SaveChanges();
                }
                if (!context.Enrollments.Any()&& useCustomizationData)
                {
                    await context.Enrollments.AddRangeAsync(GetPreConfigurationEnrollments());
                    context.SaveChanges();
                }
                if (!context.Instructors.Any())
                {
                    await context.Instructors.AddRangeAsync(GetPreConfigurationInstructors());
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static IEnumerable<Student> GetPreConfigurationStudents()
        {
            return new List<Student>()
            {
                Student.CreateNew("Carson","Alexander",DateTime.Parse("2005-09-01")),
                Student.CreateNew("Meredith","Alonso",DateTime.Parse("2002-09-01")),
                Student.CreateNew("Arturo","Anand",DateTime.Parse("2003-09-01")),
                Student.CreateNew("Gytis","Barzdukas",DateTime.Parse("2002-09-01")),
                Student.CreateNew("Yan","Li",DateTime.Parse("2002-09-01")),
                Student.CreateNew("Peggy","Justice",DateTime.Parse("2001-09-01")),
                Student.CreateNew("Laura","Norman",DateTime.Parse("2003-09-01")),
                Student.CreateNew("Nino","Olivetto",DateTime.Parse("2005-09-01"))
            };
        }

        public static IEnumerable<Course> GetPreConfigurationCourses()
        {
            return new List<Course>()
            {
                Course.CreateNew(1050,"Chemistry",3),
                Course.CreateNew(4022,"Microeconomics",3),
                Course.CreateNew(4041,"Macroeconomics",3),
                Course.CreateNew(1045,"Calculus",4),
                Course.CreateNew(3141,"Trigonometry",4),
                Course.CreateNew(2021,"Composition",3),
                Course.CreateNew(2042,"Literature",4)
            };
        }

        public static IEnumerable<Enrollment> GetPreConfigurationEnrollments()
        {
            return new List<Enrollment>()
            {
                Enrollment.CreateNew(1,4022,Grade.Pass),
                Enrollment.CreateNew(1,1050,Grade.Fail),
                Enrollment.CreateNew(1,4041,Grade.Pass),
                Enrollment.CreateNew(2,1045,Grade.Pass),
                Enrollment.CreateNew(2,3141,Grade.Fail),
                Enrollment.CreateNew(2,2021,Grade.Fail),
                Enrollment.CreateNew(3,1050),
                Enrollment.CreateNew(4,1050),
                Enrollment.CreateNew(4,4022,Grade.Fail),
                Enrollment.CreateNew(5,4041,Grade.Drop),
                Enrollment.CreateNew(6,1045),
                Enrollment.CreateNew(7,3141,Grade.Pass),
            };
        }

        public static IEnumerable<Instructor> GetPreConfigurationInstructors()
        {
            return new List<Instructor>()
            {
                Instructor.CreateNew("Kim","Abercrombie",DateTime.Parse("1995-03-11")),
                Instructor.CreateNew("Fadi","Fakhouri",DateTime.Parse("2002-07-06")),
                Instructor.CreateNew("Roger","Harui",DateTime.Parse("1998-07-01")),
                Instructor.CreateNew("Candace","Kapoor",DateTime.Parse("2001-01-15")),
                Instructor.CreateNew("Roger","Zheng",DateTime.Parse("2004-02-12"))
            };
        }
    }
}