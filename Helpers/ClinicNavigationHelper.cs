using CardManager.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Helpers
{
    public class ClinicNavigationHelper
    {
        private readonly ApplicationDbContext _context;

        public ClinicNavigationHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public Dictionary<string, Dictionary<int, string>> PopulateClinicLinks()
        {
            var groupResult = new Dictionary<string, Dictionary<int, string>>();
            var groups = _context.ClinicGroups.Include("Clinics");
            foreach (var group in groups)
            {
                var name = group.ShortName;
                var clinics = new Dictionary<int, string>();
                foreach (var clinic in group.Clinics)
                {
                    clinics.Add(clinic.Id, clinic.Name);
                }
                groupResult.Add(name, clinics);
            }
            return groupResult;
        }
    }
}