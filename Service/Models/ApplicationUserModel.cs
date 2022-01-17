using Microsoft.AspNetCore.Identity;

namespace CardManager.Service.Models
{
    public class ApplicationUserModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string RoleId { get; set; }

        public string Role { get; set; }
    }
}
