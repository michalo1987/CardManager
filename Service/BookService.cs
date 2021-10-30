using CardManager.Data;
using CardManager.Models;
using CardManager.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Service
{
    public class BookService : IBookService
    {
        public readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(Book book)
        {
            _context.Books.Add(book);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var result = _context.Books.FirstOrDefault(b => b.Id == id);
            _context.Books.Remove(result);
            return _context.SaveChanges() > 0;
        }

        public Book Get(int? id)
        {
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }

        public IList<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public bool Update(Book book)
        {
            _context.Books.Update(book);
            return _context.SaveChanges() > 0;
        }
    }
}
