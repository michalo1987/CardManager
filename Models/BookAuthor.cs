using System.ComponentModel.DataAnnotations;

namespace CardManager.Models
{
    public class BookAuthor
    {
        [Key]
        public int BookId { get; set; }

        [Key]
        public int AuthorId { get; set; }

        public Book Book { get; set; }

        public Author Author { get; set; }
    }
}
