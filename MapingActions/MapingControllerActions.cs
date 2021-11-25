using CardManager.Models.ViewModels;
using CardManager.Service.Interfaces;
using CardManager.Service.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace CardManager.MapingActions
{
    public class MapingControllerActions
    {
        private readonly IPublisherService _publisherService;
        private readonly ICategoryService _categoryService;

        public MapingControllerActions(IPublisherService publisherService, ICategoryService categoryService)
        {
            _publisherService = publisherService;
            _categoryService = categoryService;
        }

        public AuthorViewModel MapBookAuthorWithNamesOnly(AuthorModel source)
            => new AuthorViewModel()
            {
                FirstName = source.FirstName,
                LastName = source.LastName
            };

        public BookViewModel InitBookViewModel()
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

        public SelectListItem MapAuthorToSelectListItem(AuthorModel item)
            => new SelectListItem() { Value = $"{item.AuthorId}", Text = item.FullName };

        public PublisherViewModel MapPublisherViewModelFromEntity(PublisherModel model)
        {
            var publisherViewModel = new PublisherViewModel()
            {
                PublisherId = model.PublisherId,
                Name = model.Name,
                Location = model.Location
            };

            return publisherViewModel;
        }

        public CategoryViewModel MapCategoryViewModelFromEntity(CategoryModel model)
        {
            var categoryViewModel = new CategoryViewModel()
            {
                CategoryId = model.CategoryId,
                Name = model.Name
            };

            return categoryViewModel;
        }

        public AuthorViewModel MapAuthorViewModelFromEntity(AuthorModel model)
        {
            var authorViewModel = new AuthorViewModel()
            {
                AuthorId = model.AuthorId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                Location = model.Location
            };

            return authorViewModel;
        }

        public BookViewModel MapBookViewModelFromEntity(BookModel model)
        {
            var bookViewModel = new BookViewModel()
            {
                BookId = model.BookId,
                ISBN = model.ISBN,
                Price = model.Price,
                Title = model.Title,
                CategoryName = model.CategoryName,
                PublisherName = model.PublisherName,
            };

            return bookViewModel;
        }
    }
}
