namespace CardManager.Service.Models
{
    public class BookDetailsModel
    {
        public int BookId { get; set; }

        public bool Exists { get; set; }

        public string Title { get; set; }

        public int NumberOfChapters { get; set; }

        public int NumberOfPages { get; set; }

        public double Weight { get; set; }
    }
}
