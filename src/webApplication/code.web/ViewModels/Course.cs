namespace code.web.ViewModels
{
    public record Course
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public int Grade { get; init; }  
    }
}