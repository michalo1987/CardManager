using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardManager.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required, StringLength(25)]
        public string FirstName { get; set; }

        [Required, StringLength(25)]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Location { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
