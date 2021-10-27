using CardManager.Models;
using CardManager.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CardManager.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = _authorService.GetAll();
            return View(result);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Author author = new Author();
            if (id == null)
            {
                return View(author);
            }
            author = _authorService.Get(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Author author)
        {
            if (ModelState.IsValid)
            {
                if (author.AuthorId == 0)
                {
                    _authorService.Create(author);
                }
                else
                {
                    _authorService.Update(author);
                }
                return RedirectToAction("Index");
            }
            return View(author);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _authorService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
