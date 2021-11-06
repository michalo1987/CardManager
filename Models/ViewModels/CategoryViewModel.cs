using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Models.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        public bool CategoryExists { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
