using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required, StringLength(45)]
        public string StreetAddress { get; set; }

        [Required, StringLength(45)]
        public string CityAddress { get; set; }

        [Required, StringLength(25)]
        public string CodeAddress { get; set; }
    }
}
