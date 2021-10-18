using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string FirstName { get; set; }

        [Required, StringLength(255)]
        public string LastName { get; set; }

        [Required, StringLength(11)]
        public string PersonelIdentityNumber { get; set; }

        public string Telephone { get; set; }

        public string PlainAddress { get; set; }

        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual IList<PatientCard> PatientCards { get; set; }
    }
}
