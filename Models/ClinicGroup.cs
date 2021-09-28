using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Models
{
    public class ClinicGroup
    {
        public int Id { get; set; }

        [Required, StringLength(45)]
        public string Name { get; set; }

        [Required, StringLength(5)]
        public string ShortName { get; set; }

        public virtual IList<Clinic> Clinics { get; set; }
    }
}
