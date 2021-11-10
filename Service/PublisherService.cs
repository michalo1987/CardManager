using CardManager.Data;
using CardManager.Models;
using CardManager.Service.Interfaces;
using CardManager.Service.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CardManager.Service
{
    public class PublisherService : IPublisherService
    {
        private readonly ApplicationDbContext _context;

        public PublisherService(ApplicationDbContext context)
        {
            _context = context;
        }

        public PublisherModel CreatePublisher(string name, string location)
        {
            var publisher = new Publisher()
            {
                Name = name,
                Location = location
            };

            _context.Publishers.Add(publisher);
            _context.SaveChanges();

            return new PublisherModel()
            {
                PublisherId = publisher.Id,
                Name = publisher.Name,
                Location = publisher.Location
            };
        }

        public PublisherModel DeletePublisher(int publisherId)
        {
            var publisher = _context.Publishers.
                FirstOrDefault(p => p.Id == publisherId);

            _context.Publishers.Remove(publisher);
            _context.SaveChanges();

            return publisher != null
                ? MapFromEntity(publisher)
                : null;
        }

        public PublisherModel GetPublisher(int publisherId)
        {
            var publisher = _context.Publishers
                .FirstOrDefault(p => p.Id == publisherId);

            return publisher != null
                ? MapFromEntity(publisher)
                : null;
        }

        public IEnumerable<PublisherModel> GetAll()
        {
            var pubModelList = new List<PublisherModel>();
            var pubList = _context.Publishers.ToList();

            foreach (var model in pubList)
            {
                var pubModel = new PublisherModel()
                {
                    PublisherId = model.Id,
                    Name = model.Name,
                    Location = model.Location
                };
                pubModelList.Add(pubModel);
            }
            return pubModelList;
        }

        public void PopulatePublisher()
        {
            List<Book> objList = _context.Books
                .Include(p => p.Publisher)
                .ToList();
        }

        public IEnumerable<SelectListItem> PublisherList()
        {
            return _context.Publishers
                .Select(p => new SelectListItem { Text = p.Name, Value = p.Id.ToString() });
        }

        public PublisherModel UpdatePublisher(PublisherModel model)
        {
            var publisher = _context.Publishers
                .SingleOrDefault(p => p.Id == model.PublisherId);

            publisher.Name = model.Name;
            publisher.Location = model.Location;
                
            _context.Publishers.Update(publisher);
            _context.SaveChanges();

            return model;
        }

        private static PublisherModel MapFromEntity(Publisher entity)
        {
            return new PublisherModel()
            {
                PublisherId = entity.Id,
                Name = entity.Name,
                Location = entity.Location
            };
        }
    }
}
