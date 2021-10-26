using CardManager.Data;
using CardManager.Models;
using CardManager.Service.Interfaces;
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
            var publisher = _context.Publishers.FirstOrDefault(p => p.PublisherId == id);
            _context.Publishers.Remove(publisher);
            return _context.SaveChanges() > 0;
        }

        public Publisher Get(int? id)
        {
            return _context.Publishers.FirstOrDefault(p => p.PublisherId == id);
        }

        public IList<Publisher> GetAll()
        {
            return _context.Publishers.ToList();
        }

        public bool Update(Publisher publisher)
        {
            _context.Publishers.Update(publisher);
            return _context.SaveChanges() > 0;
        }
    }
}
