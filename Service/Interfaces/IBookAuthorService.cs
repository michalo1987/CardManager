using CardManager.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Service.Interfaces
{
    public interface IBookAuthorService
    {
        BookAuthorModel GetBookAuthor(int bookId);

        bool AssignAuthor(int bookId, int authorId);

        bool DeleteBookAuthor(int bookId, int authorId);
    }
}
