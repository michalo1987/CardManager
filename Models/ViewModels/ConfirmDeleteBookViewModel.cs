using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Models.ViewModels
{
    public class ConfirmDeleteBookViewModel
    {
        public int BookId { get; set; }

        public string CategoryName { get; set; }

        public string PublisherName { get; set; }

        public string Title { get; set; }

        public string ISBN { get; set; }

        public double Price { get; set; }
    }
}
