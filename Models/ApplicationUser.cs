using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CardManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, StringLength(25)]
        public string Name { get; set; }
    }
}
