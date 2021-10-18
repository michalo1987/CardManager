using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Models
{
    public class PatientCard
    {
        public int Id { get; set; }

        public string CardNumber { get; set; }

        public int PatientId { get; set; }

        public virtual Patient Patient { get; set; }

        public int ClinicId { get; set; }

        public virtual Clinic Clinic { get; set; }

        public virtual IList<CardIssue> CardIssues { get; set; } = new List<CardIssue>();
    }
}
