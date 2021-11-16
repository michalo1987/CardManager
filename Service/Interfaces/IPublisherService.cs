using CardManager.Models;
using CardManager.Service.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CardManager.Service.Interfaces
{
    public interface IPublisherService
    {
        IEnumerable<PublisherModel> GetAll();

        PublisherModel GetPublisher(int publisherId);

        PublisherModel CreatePublisher(string publisherName, string publisherLocation);

        PublisherModel UpdatePublisher(PublisherModel model);

        PublisherModel DeletePublisher(int publisherId);

        IEnumerable<SelectListItem> PublisherList();

        int CountPublishers();
    }
}
