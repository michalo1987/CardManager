using System;

namespace CardManager.Service.Models
{
    public class AuthorModel
    {
        public int AuthorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        public DateTime BirthDate { get; set; }

        public string Location { get; set; }
    }
}
