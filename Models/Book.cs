using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [MaxLength(15)]
        public string ISBN { get; set; }

        [Required]
        public double Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int BookDetailId { get; set; }

        public BookDetail BookDetail { get; set; }
    }
}
