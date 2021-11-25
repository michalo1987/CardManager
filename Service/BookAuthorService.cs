using CardManager.Data;
using CardManager.MapingActions;
using CardManager.Models;
using CardManager.Service.Interfaces;
using CardManager.Service.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CardManager.Service
{
    public class BookAuthorService : IBookAuthorService
    {
        private readonly ApplicationDbContext _context;
        private readonly MapingServiceActions _maping;

        public BookAuthorService(ApplicationDbContext context, MapingServiceActions maping)
        {
            _context = context;
            _maping = maping;
        }

        public BookAuthorModel GetBookAuthor(int bookId)
        {
            var book = _context.Books
                .Include(book => book.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .SingleOrDefault(book => book.Id == bookId);

            return book != null
                ? _maping.MapBookAuthorModelFromEntity(book)
                : null;
        }

        public bool AssignAuthor(int bookId, int authorId)
        {
            try
            {
                var bookAuthorAssignment = new BookAuthor()
                { 
                    BookId = bookId,
                    AuthorId = authorId
                };

                _context.BookAuthors.Add(bookAuthorAssignment);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteBookAuthor(int bookId, int authorId)
        {
            try
            {
                var bookAuthor = _context.BookAuthors.SingleOrDefault(ba => ba.AuthorId == authorId && ba.BookId == bookId);

                _context.BookAuthors.Remove(bookAuthor);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
