using CardManager.Data;
using CardManager.Models;
using CardManager.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(Author author)
        {
            _context.Authors.Add(author);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var author = _context.Authors.FirstOrDefault(a => a.AuthorId == id);
            _context.Authors.Remove(author);
            return _context.SaveChanges() > 0;
        }

        public Author Get(int? id)
        {
            return _context.Authors.FirstOrDefault(a => a.AuthorId == id);
        }

        public IList<Author> GetAll()
        {
            return _context.Authors.ToList();
        }

        public bool Update(Author author)
        {
            _context.Authors.Update(author);
            return _context.SaveChanges() > 0;
        }
    }
}
