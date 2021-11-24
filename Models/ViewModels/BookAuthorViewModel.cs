using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Models.ViewModels
{
    public class BookAuthorViewModel
    {
        public int BookId { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public string Title { get; set; }

        public string FullName { get; set; }

        /// <summary>
        ///     Collection of authors assigned to this book.
        /// </summary>
        public IEnumerable<AuthorViewModel> BookAuthorList { get; set; }

        /// <summary>
        ///     Collection of available authors that can be assigned to a book. 
        /// </summary>
        public IEnumerable<SelectListItem> AvailableAuthorList { get; set; }
    }
}
