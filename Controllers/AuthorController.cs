using CardManager.MapingActions.Interfaces;
using CardManager.Models.ViewModels;
using CardManager.Service.Interfaces;
using CardManager.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CardManager.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IMapingControllerActions _maping;

        public AuthorController(IAuthorService authorService, IMapingControllerActions maping)
        {
            _authorService = authorService;
            _maping = maping;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var autViewModelList = new List<AuthorViewModel>();
            var autModelList = _authorService.GetAll();

            foreach (var model in autModelList)
            {
                var viewModel = _maping.MapAuthorViewModelFromModel(model);
                autViewModelList.Add(viewModel);
            }

            return View(autViewModelList);
        }

        [HttpGet]
        public IActionResult New()
        {
            var viewModel = new AuthorViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New([FromForm]AuthorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _authorService.CreateAuthor(viewModel.FirstName, viewModel.LastName, viewModel.BirthDate, viewModel.Location);

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var author = _authorService.GetAuthor(id);
            if (author == null)
            {
                return NotFound($"Author ID = {id} does not exists.");
            }

            var viewModel = _maping.MapAuthorViewModelFromModel(author);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm]AuthorViewModel viewModel)
        {
            var model = new AuthorModel()
            {
                AuthorId = viewModel.AuthorId,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                BirthDate = viewModel.BirthDate,
                Location = viewModel.Location
            };

            if (ModelState.IsValid)
            {
                _authorService.UpdateAuthor(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _authorService.DeleteAuthor(id);

            return RedirectToAction("Index");
        }
    }
}
