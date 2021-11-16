using System.ComponentModel.DataAnnotations;

namespace CardManager.Models
{
    public class BookDetail
    {
        public int Id { get; set; }

        [Required]
        public int NumberOfChapters { get; set; }

        public int NumberOfPages { get; set; }

        public double Weight { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }
    }
}
