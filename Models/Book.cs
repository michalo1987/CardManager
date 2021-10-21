using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [MaxLength(15)]
        public string ISBN { get; set; }

        [Required]
        public double Price { get; set; }

        public int? BookDetailId { get; set; }

        public BookDetail BookDetail { get; set; }

        public int PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
