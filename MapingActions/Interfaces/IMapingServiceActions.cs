using CardManager.Models;
using CardManager.Service.Models;

namespace CardManager.MapingActions.Interfaces
{
    public interface IMapingServiceActions
    {
        CategoryModel MapCategoryModelFromEntity(Category entity);

        PublisherModel MapPublisherModelFromEntity(Publisher entity);

        AuthorModel MapAuthorModelFromEntity(Author entity);

        BookModel MapBookModelFromEntity(Book entity);

        BookDetailsModel MapBookDetailsModelFromEntity(Book entity);

        BookAuthorModel MapBookAuthorModelFromEntity(Book entity);
    }
}
