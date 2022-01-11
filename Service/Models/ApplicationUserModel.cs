using Microsoft.AspNetCore.Identity;

namespace CardManager.Service.Models
{
    public class ApplicationUserModel : IdentityUser
    {
        public string Name { get; set; }

        public string RoleId { get; set; }

        public string Role { get; set; }
    }
}
