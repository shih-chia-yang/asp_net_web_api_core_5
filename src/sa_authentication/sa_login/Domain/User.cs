using System;

namespace sa_login.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool CanManaged { get; set; }

        public bool HasExpenseCredit { get; set; }        
    }
}
