using CardManager.Service.Models;
using System;
using System.Collections.Generic;

namespace CardManager.Service.Interfaces
{
    public interface IAuthorService
    {
        IEnumerable<AuthorModel> GetAll();

        AuthorModel GetAuthor(int authorId);

        AuthorModel CreateAuthor(string firstName, string lastName, DateTime birthDate, string location);

        AuthorModel UpdateAuthor(AuthorModel model);

        AuthorModel DeleteAuthor(int authorId);
    }
}
