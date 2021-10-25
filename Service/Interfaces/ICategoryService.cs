using CardManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Service.Interfaces
{
    public interface ICategoryService
    {
        IList<Category> GetAll();

        Category Get(int? id);

        bool Create(Category category);

        bool Update(Category category);

        bool Delete(int id);

        bool CreateMultiple2(IList<Category> categories);

        bool CreateMultiple5(IList<Category> categories);

        bool RemoveMultiple2(IEnumerable<Category> categories);

        bool RemoveMultiple5(IEnumerable<Category> categories);
    }
}
