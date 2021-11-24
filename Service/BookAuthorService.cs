using CardManager.Data;
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

        public BookAuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public BookAuthorModel GetBookAuthor(int bookId)
        {
            var book = _context.Books
                .Include(book => book.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .SingleOrDefault(book => book.Id == bookId);

            return book != null
                ? MapFromEntity(book)
                : null;
        }

        private static BookAuthorModel MapFromEntity(Book entity)
        {
            return new BookAuthorModel()
            {
                BookId = entity.Id,
                Title = entity.Title,
                AuthorList = entity
                    .BookAuthors
                    .Select(a => MapFromAuthorEntity(a.Author))
                    .ToList()
            };
        }

        private static AuthorModel MapFromAuthorEntity(Author entity)
        {
            return new AuthorModel()
            {
                AuthorId = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                BirthDate = entity.BirthDate,
                Location = entity.Location
            };
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
