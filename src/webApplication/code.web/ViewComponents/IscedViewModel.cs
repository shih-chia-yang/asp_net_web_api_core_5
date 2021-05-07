using System.Collections.Generic;

namespace code.web.ViewComponents
{
    public  class IscedViewModel
    {
        public string IscedId{ get; set; }

        public string IscedName { get; set; }

        public IList<IscedViewModel> DetailedList { get; set; }
    }
}