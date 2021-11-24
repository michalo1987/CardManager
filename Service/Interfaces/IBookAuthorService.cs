using CardManager.Service.Models;

namespace CardManager.Service.Interfaces
{
    public interface IBookAuthorService
    {
        BookAuthorModel GetBookAuthor(int bookId);

        bool AssignAuthor(int bookId, int authorId);

        bool DeleteBookAuthor(int bookId, int authorId);
    }
}
