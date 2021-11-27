using CardManager.MapingActions.Interfaces;
using CardManager.Models;
using CardManager.Service.Models;
using System.Linq;

namespace CardManager.MapingActions
{
    public class MapingServiceActions : IMapingServiceActions
    {
        public CategoryModel MapCategoryModelFromEntity(Category entity)
        {
            return new CategoryModel()
            {
                CategoryId = entity.Id,
                Name = entity.Name,
            };
        }

        public PublisherModel MapPublisherModelFromEntity(Publisher entity)
        {
            return new PublisherModel()
            {
                PublisherId = entity.Id,
                Name = entity.Name,
                Location = entity.Location
            };
        }

        public AuthorModel MapAuthorModelFromEntity(Author entity)
        {
            return new AuthorModel()
            {
                AuthorId = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                BirthDate = entity.BirthDate,
                Location = entity.Location
            };
        }

        public BookModel MapBookModelFromEntity(Book entity)
        {
            return new BookModel()
            {
                BookId = entity.Id,
                PublisherId = entity.PublisherId,
                CategoryId = entity.CategoryId,
                ISBN = entity.ISBN,
                Price = entity.Price,
                Title = entity.Title,
                CategoryName = entity.Category?.Name,
                PublisherName = entity.Publisher?.Name
            };
        }

        public BookDetailsModel MapBookDetailsModelFromEntity(Book entity)
        {
            return new BookDetailsModel()
            {
                BookId = entity.Id,
                Exists = entity.BookDetail != null,
                Title = entity.Title,
                NumberOfChapters = (entity.BookDetail?.NumberOfChapters).GetValueOrDefault(),
                NumberOfPages = (entity.BookDetail?.NumberOfPages).GetValueOrDefault(),
                Weight = (entity.BookDetail?.Weight).GetValueOrDefault(),
            };
        }

        public BookAuthorModel MapBookAuthorModelFromEntity(Book entity)
        {
            return new BookAuthorModel()
            {
                BookId = entity.Id,
                Title = entity.Title,
                AuthorList = entity
                    .BookAuthors
                    .Select(a => MapAuthorModelFromEntity(a.Author))
                    .ToList()
            };
        }
    }
}
