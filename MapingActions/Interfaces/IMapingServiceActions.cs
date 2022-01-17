using CardManager.Models;
using CardManager.Service.Models;
using Microsoft.AspNetCore.Identity;

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

        ApplicationUserModel MapApplicationUserModelFromEntity(ApplicationUser entity);

        RoleModel MapRoleModelFromEntity(IdentityRole entity);
    }
}
