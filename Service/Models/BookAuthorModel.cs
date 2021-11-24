using CardManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CardManager.Service.Models
{
    public class BookAuthorModel
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public ICollection<AuthorModel> AuthorList { get; set; }
    }
}
