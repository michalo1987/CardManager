using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Service.Models
{
    public class AuthorModel
    {
        public int AuthorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Location { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }
}
