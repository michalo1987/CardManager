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
    }
}
