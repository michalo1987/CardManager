using CardManager.Models;
using CardManager.Models.ViewModels;
using CardManager.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CardManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IPublisherService _publisherService;
        private readonly IAuthorService _authorService;

        public HomeController(IBookService bookService, IPublisherService publisherService, IAuthorService authorService)
        {
            _bookService = bookService;
            _publisherService = publisherService;
            _authorService = authorService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new HomePageViewModel()
            {
                BookCount = _bookService.CountBooks(),
                PublisherCount = _publisherService.CountPublishers(),
                AuthorCount = _authorService.CountAuthors()
            };

            return View(viewModel);
        }

        public IActionResult CardManager()
        {
            return RedirectToAction("Index");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
