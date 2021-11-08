using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Models.ViewModels
{
    public class PublisherViewModel
    {
        public int PublisherId { get; set; }

        [Required, StringLength(25)]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }
    }
}
