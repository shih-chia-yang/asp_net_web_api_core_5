namespace code.Api.Extensions.Pagination
{
    public record LinkedResource(string href);

    public enum LinkedResourceType
    {
        None,Prev,Next
    }
}