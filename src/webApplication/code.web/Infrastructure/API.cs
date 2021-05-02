namespace code.web.Infrastructure
{
    public class API
    {
        public static class Student
        {
            public static string BaseUri(string baseUri) => $"{baseUri}/Student";
            public static string BaseUriWithParam(string baseUri, string studentId) => $"{baseUri}/Student/{studentId}";

        }

        public static class Instructor
        {
            public static string GetInstructorList(string baseUri)=>$"{baseUri}/Instructors";

            public static string GetInstructor(string baseUri, string InstructorId) => $"{baseUri}/Instructor/{InstructorId}";

            public static string InstructorCommand(string baseUri, string command) => $"{baseUri}/Instructor/{command}";
        }
    }
}