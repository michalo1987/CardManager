using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Models
{
    public class CardIssue
    {
        public int Id { get; set; }

        public DateTime ReleaseDate { get; set; }

        public DateTime? DateOfReturn { get; set; }

        [Required, MaxLength]
        public string Description { get; set; }

        public int PatientCardId { get; set; }

        public virtual PatientCard PatientCard { get; set; }
    }
}
