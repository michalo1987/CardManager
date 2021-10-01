﻿using System;
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

        public int ClinicGroupId { get; set; }

        public virtual ClinicGroup ClinicGroup { get; set; }
    }
}