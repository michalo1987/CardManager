using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required, StringLength(35)]
        public string Name { get; set; }
    }
}
