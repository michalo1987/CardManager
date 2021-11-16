using CardManager.Models.ViewModels;
using CardManager.Service.Interfaces;
using CardManager.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace CardManager.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IPublisherService _publisherService;
        private readonly ICategoryService _categoryService;
        private readonly IBookDetailService _bookDetailService;

        public BookController(IBookService bookService, IPublisherService publisherService, ICategoryService categoryService,
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
            var bookViewModelList = new List<BookViewModel>();
            var bookModelList = _bookService.GetAll();

            foreach (var model in bookModelList)
            {
                var bookViewModel = new BookViewModel()
                {
                    BookId = model.BookId,
                    ISBN = model.ISBN,
                    Price = model.Price,
                    Title = model.Title,
                    CategoryName = model.CategoryName,
                    PublisherName = model.PublisherName
                };
                bookViewModelList.Add(bookViewModel);
            }
            
            return View(bookViewModelList);
        }

        [HttpGet]
        public IActionResult New()
        {
            var viewModel = InitBookViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New([FromForm]BookViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _bookService.CreateBook(viewModel.Title, viewModel.ISBN, viewModel.Price, viewModel.CategoryId, viewModel.PublisherId);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _bookService.GetBook(id);
            if (book == null)
            {
                return NotFound($"Book ID = {id} does not exists.");
            }

            var viewModel = InitBookViewModel();

            viewModel.BookId = book.BookId;
            viewModel.CategoryId = book.CategoryId;
            viewModel.PublisherId = book.PublisherId;
            viewModel.Title = book.Title;
            viewModel.ISBN = book.ISBN;
            viewModel.Price = book.Price;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm]BookViewModel viewModel)
        {
            var model = new BookModel()
            {
                BookId = viewModel.BookId,
                CategoryId = viewModel.CategoryId,
                PublisherId = viewModel.PublisherId,
                ISBN = viewModel.ISBN,
                Title = viewModel.Title,
                Price = viewModel.Price
            };

            if (ModelState.IsValid)
            {
                _bookService.UpdateBook(model);
            }

            return RedirectToAction("Index");
        }

        private BookViewModel InitBookViewModel()
        {
            var bookViewModel = new BookViewModel
            {
                PublisherList = _publisherService.GetAll().Select(item => MapPublisherToSelectListItem(item)),
                CategoryList = _categoryService.GetAll().Select(item => MapCategoryToSelectListItem(item))
            };

            return bookViewModel;
        }

        private static SelectListItem MapCategoryToSelectListItem(CategoryModel item)
            => new SelectListItem() { Value = $"{item.CategoryId}", Text = item.Name };

        private static SelectListItem MapPublisherToSelectListItem(PublisherModel item)
            => new SelectListItem() { Value = $"{item.PublisherId}", Text = item.Name };

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
                Title = bookDetails.Title,
                NumberOfChapters = bookDetails.NumberOfChapters,
                NumberOfPages = bookDetails.NumberOfPages,
                Weight = bookDetails.Weight,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details([FromForm]BookDetailsViewModel bookDetailsViewModel)
        {
            var model = new BookDetailsModel()
            {
                BookId = bookDetailsViewModel.BookId,
                Title = bookDetailsViewModel.Title,
                NumberOfChapters = bookDetailsViewModel.NumberOfChapters,
                NumberOfPages = bookDetailsViewModel.NumberOfPages,
                Weight = bookDetailsViewModel.Weight
            };
            _bookDetailService.UpdateBookDetails(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromForm]ConfirmDeleteBookViewModel viewModel)
        {
            try
            {
                _bookService.DeleteBook(viewModel.BookId);
            }
            catch
            {
                return RedirectToAction("ConfirmDelete", new { id = viewModel.BookId});
            }

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            var book = _bookService.GetBook(id);
            if (book == null)
            {
                return NotFound($"Book ID = {id} does not exists.");
            }

            var viewModel = new ConfirmDeleteBookViewModel()
            {
                BookId = book.BookId,
                CategoryName = book.CategoryName,
                PublisherName = book.PublisherName,
                Title = book.Title,
                ISBN = book.ISBN,
                Price = book.Price
            };

            return View(viewModel);
        }
    }
}
