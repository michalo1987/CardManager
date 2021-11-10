using CardManager.Models;
using CardManager.Service.Models;
using System.Collections.Generic;

namespace CardManager.Service.Interfaces
{
    public interface IBookService
    {
        IEnumerable<BookModel> GetAll();

        BookModel GetBook(int bookId);

        BookModel CreateBook(string title, string isbn, double price, int categoryId, int publisherId);

        BookModel UpdateBook(BookModel model);

        BookModel DeleteBook(int bookId);
    }
}
