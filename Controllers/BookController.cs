using CardManager.MapingActions.Interfaces;
using CardManager.Models.ViewModels;
using CardManager.Service.Interfaces;
using CardManager.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CardManager.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IBookDetailService _bookDetailService;
        private readonly IBookAuthorService _bookAuthorService;
        private readonly IAuthorService _authorService;
        private readonly IMapingControllerActions _maping;

        public BookController(IBookService bookService, IBookDetailService bookDetailService, IBookAuthorService bookAuthorService, IAuthorService authorService, IMapingControllerActions maping)
        {
            _bookService = bookService;
            _bookDetailService = bookDetailService;
            _bookAuthorService = bookAuthorService;
            _authorService = authorService;
            _maping = maping;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var bookViewModelList = new List<BookViewModel>();
            var bookModelList = _bookService.GetAll();

            foreach (var model in bookModelList)
            {
                var viewModel = _maping.MapBookViewModelFromModel(model);
                viewModel.BookAuthorList = model.AuthorList.Select(a => _maping.MapBookAuthorWithNamesOnly(a));

                bookViewModelList.Add(viewModel);
            }

            return View(bookViewModelList);
        }

        [HttpGet]
        public IActionResult New()
        {
            var viewModel = _maping.InitBookViewModel();

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

            var viewModel = _maping.InitBookViewModel();

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

        [HttpGet("{controller}/{action}/{bookId}")]
        public IActionResult Details(int bookId)
        {
            var bookDetails = _bookDetailService.GetBookDetails(bookId);
            if (bookDetails == null)
            {
                return NotFound($"Book with ID = {bookId} does not exists.");
            }

            var viewModel = _maping.MapBookDetailsViewModelFromModel(bookDetails);
            viewModel.BookId = bookId;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details([FromForm]BookDetailsViewModel viewModel)
        {
            var model = new BookDetailsModel()
            {
                BookId = viewModel.BookId,
                Title = viewModel.Title,
                NumberOfChapters = viewModel.NumberOfChapters,
                NumberOfPages = viewModel.NumberOfPages,
                Weight = viewModel.Weight
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
                return RedirectToAction("ConfirmDelete", new { id = viewModel.BookId });
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            var book = _bookService.GetBook(id);
            if (book == null)
            {
                return NotFound($"Book with ID = {id} does not exists.");
            }

            var viewModel = _maping.MapConfirmDeleteBookViewModelFromModel(book);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult ManageAuthors(int id)
        {
            var bookAuthor = _bookAuthorService.GetBookAuthor(id);
            if (bookAuthor == null)
            {
                return NotFound($"Book with ID = {id} does not exists.");
            }

            var availableAuthors = _authorService.GetAll();

            var viewModel = _maping.MapBookAuthorViewModelFromModel(bookAuthor);
            viewModel.AvailableAuthorList = availableAuthors
                .Where(a => bookAuthor.AuthorList.Any(ba => ba.AuthorId == a.AuthorId) == false)
                .Select(a => _maping.MapAuthorToSelectListItem(a));

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ManageAuthors([FromForm]BookAuthorViewModel viewModel)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest("400 BadRequest!");
            }

            var result = _bookAuthorService.AssignAuthor(viewModel.BookId, viewModel.AuthorId);
            if (result == false)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                return Content("Server error!");
            }

            return ManageAuthors(viewModel.BookId);
        }

        [HttpGet]
        public IActionResult RemoveAuthors([FromQuery]int bookId, [FromQuery]int authorId)
        {
            _bookAuthorService.DeleteBookAuthor(bookId, authorId);

            return RedirectToAction("ManageAuthors", new { id = bookId});
        }
    }
}
