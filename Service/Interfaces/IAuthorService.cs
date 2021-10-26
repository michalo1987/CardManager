using CardManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Service.Interfaces
{
    public interface IAuthorService
    {
        IList<Author> GetAll();

        Author Get(int? id);

        bool Create(Author author);

        bool Update(Author author);

        bool Delete(int id);
    }
}
