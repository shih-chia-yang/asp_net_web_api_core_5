namespace code.Api.Application.Dto
{
    public record CourseDto
    {

        public int Id { get; init; }
        public string Title { get; init; }
        public int Grade { get; init; }  
    }
}