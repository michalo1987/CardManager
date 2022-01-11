using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, StringLength(25)]
        public string Name { get; set; }

        [NotMapped]
        public string RoleId { get; set; }

        [NotMapped]
        public string Role { get; set; }
    }
}
