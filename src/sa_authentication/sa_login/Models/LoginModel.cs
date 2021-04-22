using System.ComponentModel;
using System;

namespace sa_login.Models
{   
    public enum Roles
    {
        User,Admin
    }
    public class LoginModel
    {
        [DisplayName("帳號")]
        public string Email {get;set;}

        [DisplayName("密碼")]
        public string Password{get;set;}

        public string RequestPath { get; set; }
        public Roles Role { get; set; }
    }
}
