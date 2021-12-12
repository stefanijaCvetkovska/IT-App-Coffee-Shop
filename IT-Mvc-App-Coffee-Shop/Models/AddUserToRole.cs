using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IT_Mvc_App_Coffee_Shop.Models
{
    public class AddUserToRole
    {
        public string Email { get; set; }
        public string SelectedRole { get; set; }
        public string SelectedEmail { get; set; }
        public List<String> Roles { get; set; }
        public List<String> Emails { get; set; }
        public Profile User { get; set; }
        public AddUserToRole()
        {
            Roles = new List<string>();
            Emails = new List<string>();
        }
    }
}