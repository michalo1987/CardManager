using CardManager.Data;
using CardManager.MapingActions.Interfaces;
using CardManager.Models;
using CardManager.Service.Interfaces;
using CardManager.Service.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace CardManager.Service
{
    public class PublisherService : IPublisherService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapingServiceActions _maping;

        public PublisherService(ApplicationDbContext context, IMapingServiceActions maping)
        {
            _context = context;
            _maping = maping;
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

            return _maping.MapPublisherModelFromEntity(publisher);
        }

        public PublisherModel DeletePublisher(int publisherId)
        {
            var publisher = _context.Publishers.
                FirstOrDefault(p => p.Id == publisherId);

            _context.Publishers.Remove(publisher);
            _context.SaveChanges();

            return publisher != null
                ? _maping.MapPublisherModelFromEntity(publisher)
                : null;
        }

        public PublisherModel GetPublisher(int publisherId)
        {
            var publisher = _context.Publishers
                .FirstOrDefault(p => p.Id == publisherId);

            return publisher != null
                ? _maping.MapPublisherModelFromEntity(publisher)
                : null;
        }

        public IEnumerable<PublisherModel> GetAll()
        {
            var pubModelList = new List<PublisherModel>();
            var pubList = _context.Publishers.ToList();

            foreach (var model in pubList)
            {
                var pubModel = _maping.MapPublisherModelFromEntity(model);
                pubModelList.Add(pubModel);
            }

            return pubModelList;
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

        public int CountPublishers()
        {
            return _context.Publishers.Count();
        }
    }
}
