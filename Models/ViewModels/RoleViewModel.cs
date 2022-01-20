using System.ComponentModel.DataAnnotations;

namespace CardManager.Models.ViewModels
{
    public class RoleViewModel
    {
        public string RoleId { get; set; }

        [Required, StringLength(25)]
        public string RoleName { get; set; }

        public string NormalizedName { get; set; }
    }
}
