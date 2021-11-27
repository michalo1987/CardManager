using CardManager.Data;
using CardManager.MapingActions.Interfaces;
using CardManager.Models;
using CardManager.Service.Interfaces;
using CardManager.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardManager.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapingServiceActions _maping;

        public CategoryService(ApplicationDbContext context, IMapingServiceActions maping)
        {
            _context = context;
            _maping = maping;
        }

        public IEnumerable<CategoryModel> GetAll()
        {
            var catModelList = new List<CategoryModel>();
            var catlist = _context.Categories.ToList();

            foreach (var model in catlist)
            {
                var catModel = _maping.MapCategoryModelFromEntity(model);
                catModelList.Add(catModel);
            }

            return catModelList;
        }

        public CategoryModel GetCategory(int categoryId)
        {
            var category = _context.Categories
                .FirstOrDefault(c => c.Id == categoryId);

            return category != null
                ? _maping.MapCategoryModelFromEntity(category)
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

            return _maping.MapCategoryModelFromEntity(category);
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
                ? _maping.MapCategoryModelFromEntity(category)
                : null;
        }

        public bool CreateMultiple2()
        {
            var catList = new List<Category>();
            for (int i = 1; i <= 2; i++)
            {
                catList.Add(new Category { Name = Guid.NewGuid().ToString() });
            }
            _context.Categories.AddRange(catList);
            _context.SaveChanges();

            return true;
        }

        public bool CreateMultiple5()
        {
            var catList = new List<Category>();
            for (int i = 1; i <= 5; i++)
            {
                catList.Add(new Category { Name = Guid.NewGuid().ToString() });
            }
            _context.Categories.AddRange(catList);
            _context.SaveChanges();

            return true;
        }

        public bool RemoveMultiple2()
        {
            var catList = _context.Categories.OrderByDescending(c => c.Id).Take(2).ToList();
            _context.Categories.RemoveRange(catList);

            _context.SaveChanges();

            return true;
        }

        public bool RemoveMultiple5()
        {
            var catList = _context.Categories.OrderByDescending(c => c.Id).Take(5).ToList();
            _context.Categories.RemoveRange(catList);

            _context.SaveChanges();

            return true;
        }
    }
}
