using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, StringLength(25)]
        public string Name { get; set; }
    }
}
