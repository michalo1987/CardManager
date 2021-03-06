using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardManager.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [MaxLength(13)]
        public string ISBN { get; set; }

        [Required]
        public double Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [NotMapped]
        public BookDetail BookDetail { get; set; }

        public int PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
