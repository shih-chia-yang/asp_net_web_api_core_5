using System;
using Microsoft.AspNetCore.Authorization;

namespace sa_login.Auth
{
    public class ManagerRequirement : IAuthorizationRequirement
    {
        public bool IsAdmin { get; set; }
        public ManagerRequirement(bool isAdmin)
        {
            IsAdmin = isAdmin;
        }
    }
}
