using CardManager.Data;
using CardManager.MapingActions.Interfaces;
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
        private readonly IMapingServiceActions _maping;

        public BookDetailService(ApplicationDbContext context, IMapingServiceActions maping)
        {
            _context = context;
            _maping = maping;
        }

        public BookDetailsModel GetBookDetails(int bookId)
        {
            var book = _context.Books
                .Include(book => book.BookDetail)
                .FirstOrDefault(book => book.Id == bookId);

            return book != null
                ? _maping.MapBookDetailsModelFromEntity(book)
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
    }
}
