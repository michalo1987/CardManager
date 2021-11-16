using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardManager.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        [Required, StringLength(25)]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        public List<Book> Books { get; set; }
    }
}
