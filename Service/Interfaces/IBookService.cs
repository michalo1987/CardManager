using CardManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Service.Interfaces
{
    public interface IBookService
    {
        IList<Book> GetAll();

        Book Get(int? id);

        bool Create(Book book);

        bool Update(Book book);

        bool Delete(int id);
    }
}
