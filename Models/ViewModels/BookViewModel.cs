using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CardManager.Models.ViewModels
{
    public class BookViewModel
    {
        public Book Book { get; set; }

        public IEnumerable<SelectListItem> PublisherList { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
