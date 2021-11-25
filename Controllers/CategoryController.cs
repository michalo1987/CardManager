using CardManager.Models;
using CardManager.Models.ViewModels;
using CardManager.Service.Interfaces;
using CardManager.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace CardManager.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var catViewModelList = new List<CategoryViewModel>();
            var catModelList = _categoryService.GetAll();

            foreach (var model in catModelList)
            {
                var viewModel = new CategoryViewModel()
                {
                    CategoryId = model.CategoryId,
                    Name = model.Name
                };
                catViewModelList.Add(viewModel);
            }

            return View(catViewModelList);
        }

        [HttpGet]
        public IActionResult New()
        {
            var viewModel = new CategoryViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New([FromForm] CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _categoryService.CreateCategory(viewModel.Name);

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetCategory(id);
            if (category == null)
            {
                return NotFound($"Category ID = {id} does not exists.");
            }

            var viewModel = new CategoryViewModel()
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm] CategoryViewModel viewModel)
        {
            var model = new CategoryModel()
            {
                CategoryId = viewModel.CategoryId,
                Name = viewModel.Name,
            };

            if (ModelState.IsValid)
            {
                _categoryService.UpdateCategory(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateMultiple2()
        {
            _categoryService.CreateMultiple2();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateMultiple5()
        {
            _categoryService.CreateMultiple5();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult RemoveMultiple2()
        {
            _categoryService.RemoveMultiple2();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult RemoveMultiple5()
        {
            _categoryService.RemoveMultiple5();

            return RedirectToAction("Index");
        }
    }
}
