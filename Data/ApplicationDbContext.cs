using CardManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<CardIssue> CardIssues { get; set; }

        public DbSet<Clinic> Clinics { get; set; }

        public DbSet<ClinicGroup> ClinicGroups { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<PatientCard> PatientCards { get; set; }
    }
}
