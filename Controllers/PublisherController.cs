using CardManager.Models;
using CardManager.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CardManager.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _publisherService.GetAll();
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Publisher publisher = new Publisher();
            if (id == null)
            {
                return View(publisher);
            }
            publisher = _publisherService.Get(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                if (publisher.PublisherId == 0)
                {
                    _publisherService.Create(publisher);
                }
                else
                {
                    _publisherService.Update(publisher);
                }
                return RedirectToAction("Index");
            }
            return View(publisher);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _publisherService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
