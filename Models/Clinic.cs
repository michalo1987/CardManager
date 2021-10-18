using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Models
{
    public class Clinic
    {
        public int Id { get; set; }

        [Required, StringLength(45)]
        public string Name { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public int ClinicGroupId { get; set; }

        public virtual ClinicGroup ClinicGroup { get; set; }

        public virtual IList<PatientCard> PatientCards { get; set; }
    }
}
