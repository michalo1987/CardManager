using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Models.ViewModels
{
    public class BookDetailsViewModel
    {
        public int BookId { get; set; }

        public bool DetailsExists { get; set; }
        
        public int NumberOfChapters { get; set; }

        public int NumberOfPages { get; set; }

        public double Weight { get; set; }
    }
}
