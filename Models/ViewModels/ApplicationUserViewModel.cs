using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Models.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string UserId { get; set; }

        public string RoleId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}
