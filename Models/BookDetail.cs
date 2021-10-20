using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Models
{
    public class BookDetail
    {
        public int Id { get; set; }

        [Required]
        public int NumberOfChapters { get; set; }

        public int NumberOfPages { get; set; }

        public double Weight { get; set; }

        public Book Book { get; set; }
    }
}
