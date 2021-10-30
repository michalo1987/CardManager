using CardManager.Data;
using CardManager.Models;
using CardManager.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public Category Get(int? id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public bool Create(Category category)
        {
            _context.Categories.Add(category);
            return _context.SaveChanges() > 0;
        }

        public bool Update(Category category)
        {
            _context.Categories.Update(category);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            _context.Categories.Remove(category);
            return _context.SaveChanges() > 0;
        }

        public bool CreateMultiple2(IList<Category> categories)
        {
            var catList = new List<Category>();
            for (int i = 1; i <= 2; i++)
            {
                catList.Add(new Category { Name = Guid.NewGuid().ToString() });
            }
            _context.Categories.AddRange(catList);
            return _context.SaveChanges() > 0;
        }

        public bool CreateMultiple5(IList<Category> categories)
        {
            var catList = new List<Category>();
            for (int i = 1; i <= 5; i++)
            {
                catList.Add(new Category { Name = Guid.NewGuid().ToString() });
            }
            _context.Categories.AddRange(catList);
            return _context.SaveChanges() > 0;
        }

        public bool RemoveMultiple2(IEnumerable<Category> categories)
        {
            var catList = _context.Categories.OrderByDescending(c => c.Id).Take(2).ToList();
            _context.Categories.RemoveRange(catList);
            return _context.SaveChanges() > 0;
        }

        public bool RemoveMultiple5(IEnumerable<Category> categories)
        {
            var catList = _context.Categories.OrderByDescending(c => c.Id).Take(5).ToList();
            _context.Categories.RemoveRange(catList);
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<SelectListItem> CategoryList()
        {
            return _context.Categories.Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() });
        }

        public void PopulateCategory()
        {
            List<Book> objList = _context.Books.ToList();
            foreach (var obj in objList)
            {
                _context.Entry(obj).Reference(p => p.Category).Load();
            }
        }
    }
}
