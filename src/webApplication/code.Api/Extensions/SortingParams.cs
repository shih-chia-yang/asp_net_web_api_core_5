namespace code.Api.Extensions
{
    public class SortingParams
    {
        public string SortBy { get; set; } = string.Empty;
        public bool IsAscending { get; set; }

        public SortingParams()
        {
            
        }

        public SortingParams(string sorting,bool isAscending)
        {
            SortBy = sorting;
            IsAscending = isAscending;
        }
    }
}