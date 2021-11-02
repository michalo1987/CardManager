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
            var result = _categoryService.GetAll();
            return View(result);
        }

        [HttpGet]
        public IActionResult New()
        {
            var categoryViewModel = new CategoryViewModel();
            return View(categoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.CategoryId == 0)
                {
                    _categoryService.CreateCategory(model);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetCategory(id);
            if (category == null)
            {
                return NotFound($"Category ID = {id} does not exists.");
            }

            var categoryViewModel = new CategoryViewModel()
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
            };

            return View(categoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm]CategoryViewModel categoryViewModel)
        {
            var model = new CategoryModel()
            {
                CategoryId = categoryViewModel.CategoryId,
                Name = categoryViewModel.Name,
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

        public IActionResult CreateMultiple2(IList<Category> categories)
        {
            _categoryService.CreateMultiple2(categories);
            return RedirectToAction("Index");
        }

        public IActionResult CreateMultiple5(IList<Category> categories)
        {
            _categoryService.CreateMultiple5(categories);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveMultiple2(IEnumerable<Category> categories)
        {
            _categoryService.RemoveMultiple2(categories);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveMultiple5(IEnumerable<Category> categories)
        {
            _categoryService.RemoveMultiple5(categories);
            return RedirectToAction("Index");
        }
    }
}
