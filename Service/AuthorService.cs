using CardManager.Data;
using CardManager.MapingActions.Interfaces;
using CardManager.Models;
using CardManager.Service.Interfaces;
using CardManager.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardManager.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapingServiceActions _maping;

        public AuthorService(ApplicationDbContext context, IMapingServiceActions maping)
        {
            _context = context;
            _maping = maping;
        }

        public AuthorModel CreateAuthor(string firstName, string lastName, DateTime birthDate, string location)
        {
            var author = new Author()
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                Location = location
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

            return _maping.MapAuthorModelFromEntity(author);
        }

        public AuthorModel DeleteAuthor(int authorId)
        {
            var author = _context.Authors
                .FirstOrDefault(a => a.Id == authorId);
            
            _context.Authors.Remove(author);
            _context.SaveChanges();

            return author != null
                ? _maping.MapAuthorModelFromEntity(author)
                : null;
        }

        public AuthorModel GetAuthor(int authorId)
        {
            var author = _context.Authors
                .FirstOrDefault(a => a.Id == authorId);

            return author != null
                ? _maping.MapAuthorModelFromEntity(author)
                : null;
        }

        public IEnumerable<AuthorModel> GetAll()
        {
            var autModelList = new List<AuthorModel>();
            var autList = _context.Authors.ToList();

            foreach (var model in autList)
            {
                var autModel = _maping.MapAuthorModelFromEntity(model);
                autModelList.Add(autModel);
            }

            return autModelList;
        }

        public AuthorModel UpdateAuthor(AuthorModel model)
        {
            var author = _context.Authors
                .SingleOrDefault(a => a.Id == model.AuthorId);

            author.FirstName = model.FirstName;
            author.LastName = model.LastName;
            author.BirthDate = model.BirthDate;
            author.Location = model.Location;

            _context.Authors.Update(author);
            _context.SaveChanges();

            return model;
        }

        public int CountAuthors()
        {
            return _context.Authors.Count();
        }
    }
}
