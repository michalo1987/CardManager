using CardManager.Data;
using CardManager.Models;
using CardManager.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Service
{
    public class PublisherService : IPublisherService
    {
        private readonly ApplicationDbContext _context;

        public PublisherService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var publisher = _context.Publishers.FirstOrDefault(p => p.Id == id);
            _context.Publishers.Remove(publisher);
            return _context.SaveChanges() > 0;
        }

        public Publisher Get(int? id)
        {
            return _context.Publishers.FirstOrDefault(p => p.Id == id);
        }

        public IList<Publisher> GetAll()
        {
            return _context.Publishers.ToList();
        }

        public void PopulatePublisher()
        {
            List<Book> objList = _context.Books.ToList();
            foreach (var obj in objList)
            {
                _context.Entry(obj).Reference(p => p.Publisher).Load();
            }
        }

        public IEnumerable<SelectListItem> PublisherList()
        {
            return _context.Publishers.Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() });
        }

        public bool Update(Publisher publisher)
        {
            _context.Publishers.Update(publisher);
            return _context.SaveChanges() > 0;
        }
    }
}
