using CardManager.MapingActions.Interfaces;
using CardManager.Models.ViewModels;
using CardManager.Service.Interfaces;
using CardManager.Service.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace CardManager.MapingActions
{
    public class MapingControllerActions : IMapingControllerActions
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

        public PublisherViewModel MapPublisherViewModelFromModel(PublisherModel model)
        {
            var publisherViewModel = new PublisherViewModel()
            {
                PublisherId = model.PublisherId,
                Name = model.Name,
                Location = model.Location
            };

            return publisherViewModel;
        }

        public CategoryViewModel MapCategoryViewModelFromModel(CategoryModel model)
        {
            var categoryViewModel = new CategoryViewModel()
            {
                CategoryId = model.CategoryId,
                Name = model.Name
            };

            return categoryViewModel;
        }

        public AuthorViewModel MapAuthorViewModelFromModel(AuthorModel model)
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

        public BookViewModel MapBookViewModelFromModel(BookModel model)
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

        public BookDetailsViewModel MapBookDetailsViewModelFromModel(BookDetailsModel model)
        {
            var bookDetailsViewModel = new BookDetailsViewModel()
            {
                DetailsExists = model.Exists,
                Title = model.Title,
                NumberOfChapters = model.NumberOfChapters,
                NumberOfPages = model.NumberOfPages,
                Weight = model.Weight
            };

            return bookDetailsViewModel;
        }

        public ConfirmDeleteBookViewModel MapConfirmDeleteBookViewModelFromModel(BookModel model)
        {
            var confirmDeleteBookViewModel = new ConfirmDeleteBookViewModel()
            {
                BookId = model.BookId,
                CategoryName = model.CategoryName,
                PublisherName = model.PublisherName,
                Title = model.Title,
                ISBN = model.ISBN,
                Price = model.Price
            };

            return confirmDeleteBookViewModel;
        }

        public BookAuthorViewModel MapBookAuthorViewModelFromModel(BookAuthorModel model)
        {
            var bookAuthorViewModel = new BookAuthorViewModel()
            {
                BookId = model.BookId,
                Title = model.Title,
                BookAuthorList = model.AuthorList
                    .Select(a => MapAuthorViewModelFromModel(a))
            };

            return bookAuthorViewModel;
        }

        public ApplicationUserViewModel MapApplicationUserViewModelFromModel(ApplicationUserModel model)
        {
            var applicationUserViewModel = new ApplicationUserViewModel()
            {
                RoleId = model.RoleId,
                UserName = model.UserName,
                Email = model.Email,
                Role = model.Role
            };

            return applicationUserViewModel;
        }

        public RoleViewModel MapRoleViewModelFromModel(RoleModel model)
        {
            var roleViewModel = new RoleViewModel()
            {
                RoleId = model.RoleId,
                RoleName = model.RoleName,
                NormalizedName = model.NormalizedName
            };

            return roleViewModel;
        }
    }
}
