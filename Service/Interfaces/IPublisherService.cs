using CardManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Service.Interfaces
{
    public interface IPublisherService
    {
        IList<Publisher> GetAll();

        Publisher Get(int? id);

        bool Create(Publisher publisher);

        bool Update(Publisher publisher);

        bool Delete(int id);

        IEnumerable<SelectListItem> PublisherList();

        void PopulatePublisher();
    }
}
