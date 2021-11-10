using CardManager.Data;
using CardManager.Models;
using CardManager.Service.Interfaces;
using CardManager.Service.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CardManager.Service
{
    public class BookService : IBookService
    {
        public readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public BookModel CreateBook(string title, string isbn, double price, int categoryId, int publisherId)
        {
            var book = new Book()
            {
                Title = title,
                ISBN = isbn,
                Price = price,
                CategoryId = categoryId,
                PublisherId = publisherId
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            return MapFromEntity(book);
        }

        public BookModel DeleteBook(int bookId)
        {
            var book = _context.Books
                .FirstOrDefault(b => b.Id == bookId);

            _context.Books.Remove(book);
            _context.SaveChanges();

            return book != null
                ? MapFromEntity(book)
                : null;
        }

        public BookModel GetBook(int bookId)
        {
            var book = _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .FirstOrDefault(b => b.Id == bookId);

            return book != null
                ? MapFromEntity(book)
                : null;
        }

        public IEnumerable<BookModel> GetAll()
        {
            var bookModelList = new List<BookModel>();
            var bookList = _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .ToList();

            foreach (var model in bookList)
            {
                var bookModel = new BookModel()
                {
                    BookId = model.Id,
                    ISBN = model.ISBN,
                    Price = model.Price,
                    Title = model.Title,
                    CategoryName = model.Category?.Name,
                    PublisherName = model.Publisher?.Name
                };
                bookModelList.Add(bookModel);
            }
            return bookModelList;
        }

        public BookModel UpdateBook(BookModel model)
        {
            var book = _context.Books
                .SingleOrDefault(b => b.Id == model.BookId);

            book.ISBN = model.ISBN;
            book.Price = model.Price;
            book.Title = model.Title;

            _context.Books.Update(book);
            _context.SaveChanges();

            return model;
        }

        private BookModel MapFromEntity(Book entity)
        {
            return new BookModel()
            {
                BookId = entity.Id,
                PublisherId = entity.PublisherId,
                CategoryId = entity.CategoryId,
                PublisherName = entity.Publisher.Name,
                CategoryName = entity.Category.Name,
                ISBN = entity.ISBN,
                Price = entity.Price,
                Title = entity.Title
            };
        }
    }
}
