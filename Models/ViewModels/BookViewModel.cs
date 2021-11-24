using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardManager.Models.ViewModels
{
    public class BookViewModel
    {
        public int BookId { get; set; }

        public int PublisherId { get; set; }

        public int CategoryId { get; set; }

        public int AuthorId { get; set; }

        public string CategoryName { get; set; }

        public string PublisherName { get; set; }

        public string AuthorName { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(13), MaxLength(13)]
        public string ISBN { get; set; }

        [Required]
        public double Price { get; set; }

        public IEnumerable<SelectListItem> PublisherList { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> CategoryList { get; set; } = new List<SelectListItem>();

        public IEnumerable<AuthorViewModel> BookAuthorList { get; set; } = new List<AuthorViewModel>();
    }
}
