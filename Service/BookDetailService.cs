using CardManager.Data;
using CardManager.Models;
using CardManager.Service.Interfaces;
using CardManager.Service.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace CardManager.Service
{
    public class BookDetailService : IBookDetailService
    {
        private readonly ApplicationDbContext _context;

        public BookDetailService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(BookDetail bookDetail)
        {
            _context.BookDetails
                .Add(bookDetail.Book.BookDetail);

            return _context.SaveChanges() > 0;
        }

        public BookDetailsModel GetBookDetails(int bookId)
        {
            var book = _context.Books
                .Include(book => book.BookDetail)
                .FirstOrDefault(book => book.Id == bookId);

            return book != null
                ? MapFromEntity(book)
                : null;
        }

        public BookDetailsModel UpdateBookDetails(BookDetailsModel model)
        {
            var bookDetails = _context.BookDetails
                .SingleOrDefault(bd => bd.BookId == model.BookId)
                ?? new BookDetail() { BookId = model.BookId };

            bookDetails.NumberOfChapters = model.NumberOfChapters;
            bookDetails.NumberOfPages = model.NumberOfPages;
            bookDetails.Weight = model.Weight;

            _context.BookDetails.Update(bookDetails);
            _context.SaveChanges();

            return model;
        }

        public bool Update(BookDetail bookDetail)
        {
            _context.BookDetails.Update(bookDetail.Book.BookDetail);
            return _context.SaveChanges() > 0;
        }

        private static BookDetailsModel MapFromEntity(Book entity)
        {
            return new BookDetailsModel()
            {
                BookId = entity.Id,
                Exists = entity.BookDetail != null,
                Title = entity.Title,
                NumberOfChapters = (entity.BookDetail?.NumberOfChapters).GetValueOrDefault(),
                NumberOfPages = (entity.BookDetail?.NumberOfPages).GetValueOrDefault(),
                Weight = (entity.BookDetail?.Weight).GetValueOrDefault(),
            };
        }
    }
}
