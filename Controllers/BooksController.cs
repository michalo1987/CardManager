using CardManager.Models.ViewModels;
using CardManager.Service.Interfaces;
using CardManager.Service.Models;
using Microsoft.AspNetCore.Mvc;


namespace CardManager.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IPublisherService _publisherService;
        private readonly ICategoryService _categoryService;
        private readonly IBookDetailService _bookDetailService;

        public BooksController(IBookService bookService, IPublisherService publisherService, ICategoryService categoryService,
            IBookDetailService bookDetailService)
        {
            _bookService = bookService;
            _publisherService = publisherService;
            _categoryService = categoryService;
            _bookDetailService = bookDetailService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = _bookService.GetAll();
            _publisherService.PopulatePublisher();
            _categoryService.PopulateCategory();
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert([FromForm]BookViewModel bookViewModel)
        {
            if (ModelState.IsValid)
            {
                if (bookViewModel.Book.Id == 0)
                {
                    _bookService.Create(bookViewModel.Book);
                }
                else
                {
                    _bookService.Update(bookViewModel.Book);
                }
                return RedirectToAction("Index");
            }
            return View(bookViewModel);
        }

        [HttpGet("{controller}/{action}")]
        public IActionResult New()
        {
            return View("Upsert", InitBookViewModel());
        }

        [HttpGet("{controller}/{action}/{id}")]
        public IActionResult Edit(int id)
        {
            var book = _bookService.Get(id);
            if (book == null)
            {
                return NotFound();
            }

            var bookViewModel = InitBookViewModel();
            bookViewModel.Book = _bookService.Get(id);

            return View("Upsert", bookViewModel);
        }

        private BookViewModel InitBookViewModel()
        {
            var bookViewModel = new BookViewModel
            {
                PublisherList = _publisherService.PublisherList(),
                CategoryList = _categoryService.CategoryList()
            };

            return bookViewModel;
        }

        [HttpGet("{controller}/{action}/{bookId}")]
        public IActionResult Details(int bookId)
        {
            var bookDetails = _bookDetailService.GetBookDetails(bookId);
            if (bookDetails == null)
            {
                return NotFound($"Book with ID = {bookId} does not exists.");
            }

            var viewModel = new BookDetailsViewModel()
            { 
                BookId = bookId,
                DetailsExists = bookDetails.Exists,
                NumberOfChapters = bookDetails.NumberOfChapters,
                NumberOfPages = bookDetails.NumberOfPages,
                Weight = bookDetails.Weight,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(BookDetailsViewModel bookDetailsViewModel)
        {
            var model = new BookDetailsModel()
            {
                BookId = bookDetailsViewModel.BookId,
                NumberOfChapters = bookDetailsViewModel.NumberOfChapters,
                NumberOfPages = bookDetailsViewModel.NumberOfPages,
                Weight = bookDetailsViewModel.Weight
            };
            _bookDetailService.UpdateBookDetails(model);

            return RedirectToAction("Index");
        }
    }
}
