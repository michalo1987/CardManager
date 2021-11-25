using CardManager.Data;
using CardManager.MapingActions;
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
        public readonly MapingServiceActions _maping;

        public BookService(ApplicationDbContext context, MapingServiceActions maping)
        {
            _context = context;
            _maping = maping;
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

            return _maping.MapBookModelFromEntity(book);
        }

        public BookModel DeleteBook(int bookId)
        {
            var book = _context.Books
                .FirstOrDefault(b => b.Id == bookId);

            _context.Books.Remove(book);
            _context.SaveChanges();

            return book != null
                ? _maping.MapBookModelFromEntity(book)
                : null;
        }

        public BookModel GetBook(int bookId)
        {
            var book = _context.Books
                .Include(b => b.Publisher)
                .Include(b => b.Category)
                .FirstOrDefault(b => b.Id == bookId);

            return _maping.MapBookModelFromEntity(book);
        }

        public IEnumerable<BookModel> GetAll()
        {
            var bookModelList = new List<BookModel>();
            var bookList = _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .ToList();

            foreach (var model in bookList)
            {
                var bookModel = _maping.MapBookModelFromEntity(model);
                bookModel.AuthorList = model.BookAuthors
                    .Select(ba => _maping.MapAuthorModelFromEntity(ba.Author)).ToList();

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

        public int CountBooks()
        {
            return _context.Books.Count();
        }
    }
}
