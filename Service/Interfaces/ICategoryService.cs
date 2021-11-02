using CardManager.Models;
using CardManager.Service.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


namespace CardManager.Service.Interfaces
{
    public interface ICategoryService
    {
        IList<Category> GetAll();

        CategoryModel GetCategory(int categoryId);

        CategoryModel CreateCategory(CategoryModel model);

        CategoryModel UpdateCategory(CategoryModel model);

        CategoryModel DeleteCategory(int categoryId);

        bool CreateMultiple2(IList<Category> categories);

        bool CreateMultiple5(IList<Category> categories);

        bool RemoveMultiple2(IEnumerable<Category> categories);

        bool RemoveMultiple5(IEnumerable<Category> categories);

        IEnumerable<SelectListItem> CategoryList();

        void PopulateCategory();
    }
}
