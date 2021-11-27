using CardManager.Models.ViewModels;
using CardManager.Service.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CardManager.MapingActions.Interfaces
{
    public interface IMapingControllerActions
    {
        AuthorViewModel MapBookAuthorWithNamesOnly(AuthorModel source);

        BookViewModel InitBookViewModel();

        SelectListItem MapAuthorToSelectListItem(AuthorModel item);

        PublisherViewModel MapPublisherViewModelFromModel(PublisherModel model);

        CategoryViewModel MapCategoryViewModelFromModel(CategoryModel model);

        AuthorViewModel MapAuthorViewModelFromModel(AuthorModel model);

        BookViewModel MapBookViewModelFromModel(BookModel model);

        BookDetailsViewModel MapBookDetailsViewModelFromModel(BookDetailsModel model);

        ConfirmDeleteBookViewModel MapConfirmDeleteBookViewModelFromModel(BookModel model);

        BookAuthorViewModel MapBookAuthorViewModelFromModel(BookAuthorModel model);
    }
}
