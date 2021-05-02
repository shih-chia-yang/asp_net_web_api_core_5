namespace code.web.ViewModels
{
    public class SortOrder
    {
        public string SortBy { get; set; }


        public bool IsAscending { get; set; }

        public SortOrder()
        {

        }
        public SortOrder(string sortBy, bool isAscending)
        {
            this.SortBy = sortBy;
            this.IsAscending = isAscending;

        }

    }
}