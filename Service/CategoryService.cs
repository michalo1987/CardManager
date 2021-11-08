using CardManager.Data;
using CardManager.Models;
using CardManager.Service.Interfaces;
using CardManager.Service.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardManager.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CategoryModel> GetAll()
        {
            var catModelList = new List<CategoryModel>();
            var catlist = _context.Categories.ToList();

            foreach (var model in catlist)
            {
                var catModel = new CategoryModel()
                {
                    CategoryId = model.Id,
                    Name = model.Name
                };
                catModelList.Add(catModel);
            }
            return catModelList;
        }

        public CategoryModel GetCategory(int categoryId)
        {
            var category = _context.Categories
                .FirstOrDefault(c => c.Id == categoryId);

            return category != null
                ? MapFromEntity(category)
                : null;
        }

        public CategoryModel CreateCategory(string categoryName)
        {
            var category = new Category()
            {
                Name = categoryName
            };

            _context.Categories.Add(category);
            _context.SaveChanges();

            return new CategoryModel()
            {
                CategoryId = category.Id,
                Name = category.Name
            };
        }

        public CategoryModel UpdateCategory(CategoryModel model)
        {
            var category = _context.Categories
                .SingleOrDefault(c => c.Id == model.CategoryId);

            category.Name = model.Name;

            _context.Categories.Update(category);
            _context.SaveChanges();

            return model;
        }

        public CategoryModel DeleteCategory(int categoryId)
        {
            var category = _context.Categories
                .SingleOrDefault(c => c.Id == categoryId);

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return category != null
                ? MapFromEntity(category)
                : null;
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
            return _context.Categories
                .Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() });
        }

        public void PopulateCategory()
        {
            List<Book> objList = _context.Books.ToList();
            foreach (var obj in objList)
            {
                _context.Entry(obj).Reference(p => p.Category).Load();
            }
        }

        private static CategoryModel MapFromEntity(Category entity)
        {
            return new CategoryModel()
            {
                CategoryId = entity.Id,
                Name = entity.Name,
            };
        }
    }
}
