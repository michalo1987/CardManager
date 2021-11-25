using CardManager.Models;
using CardManager.Service.Models;
using System.Collections.Generic;


namespace CardManager.Service.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryModel> GetAll();

        CategoryModel GetCategory(int categoryId);

        CategoryModel CreateCategory(string categoryName);

        CategoryModel UpdateCategory(CategoryModel model);

        CategoryModel DeleteCategory(int categoryId);

        bool CreateMultiple2();

        bool CreateMultiple5();

        bool RemoveMultiple2();

        bool RemoveMultiple5();
    }
}
