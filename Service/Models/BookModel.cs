using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace CardManager.Service.Models
{
    public class BookModel
    {
        public int BookId { get; set; }

        public int PublisherId { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string PublisherName { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [MaxLength(13)]
        public string ISBN { get; set; }

        [Required]
        public double Price { get; set; }

        public IEnumerable<SelectListItem> PublisherList { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> CategoryList { get; set; } = new List<SelectListItem>();
    }
}
