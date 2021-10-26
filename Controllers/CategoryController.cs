using CardManager.Models;
using CardManager.Service.Interfaces;
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
        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if (id == null)
            {
                return View(category);
            }
            category = _categoryService.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid) 
            {
                if (category.CategoryId == 0)
                {
                    _categoryService.Create(category);
                }
                else
                {
                    _categoryService.Update(category);
                }
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);
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
