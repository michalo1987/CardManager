using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Service.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }
    }
}
