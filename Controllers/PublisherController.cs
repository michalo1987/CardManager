using CardManager.MapingActions;
using CardManager.Models.ViewModels;
using CardManager.Service.Interfaces;
using CardManager.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CardManager.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherService _publisherService;
        private readonly MapingControllerActions _maping;

        public PublisherController(IPublisherService publisherService, MapingControllerActions maping)
        {
            _publisherService = publisherService;
            _maping = maping;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var pubViewModelList = new List<PublisherViewModel>();
            var pubModelList = _publisherService.GetAll();

            foreach (var model in pubModelList)
            {
                var viewModel = _maping.MapPublisherViewModelFromEntity(model);
                pubViewModelList.Add(viewModel);
            }

            return View(pubViewModelList);
        }

        [HttpGet]
        public IActionResult New()
        {
            var viewModel = new PublisherViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New([FromForm]PublisherViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _publisherService.CreatePublisher(viewModel.Name, viewModel.Location);

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var publisher = _publisherService.GetPublisher(id);
            if (publisher == null)
            {
                return NotFound($"Publisher ID = {id} does not exists.");
            }

            var viewModel = _maping.MapPublisherViewModelFromEntity(publisher);

            return View(viewModel); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm]PublisherViewModel viewModel)
        {
            var model = new PublisherModel()
            {
                PublisherId = viewModel.PublisherId,
                Name = viewModel.Name,
                Location = viewModel.Location
            };

            if (ModelState.IsValid)
            {
                _publisherService.UpdatePublisher(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _publisherService.DeletePublisher(id);

            return RedirectToAction("Index");
        }
    }
}
