using CardManager.Data;
using CardManager.Models;
using CardManager.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Category> GetAll()
        {
            return _context.Categories.ToList();
        }
    }
}
