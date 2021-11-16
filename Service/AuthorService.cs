using CardManager.Data;
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

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
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

            return new AuthorModel()
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                BirthDate = author.BirthDate,
                Location = author.Location
            };
        }

        public AuthorModel DeleteAuthor(int authorId)
        {
            var author = _context.Authors
                .FirstOrDefault(a => a.Id == authorId);
            
            _context.Authors.Remove(author);
            _context.SaveChanges();

            return author != null
                ? MapFromEntity(author)
                : null;
        }

        public AuthorModel GetAuthor(int authorId)
        {
            var author = _context.Authors
                .FirstOrDefault(a => a.Id == authorId);

            return author != null
                ? MapFromEntity(author)
                : null;
        }

        public IEnumerable<AuthorModel> GetAll()
        {
            var autModelList = new List<AuthorModel>();
            var autList = _context.Authors.ToList();

            foreach (var model in autList)
            {
                var autModel = new AuthorModel()
                {
                    AuthorId = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDate = model.BirthDate,
                    Location = model.Location
                };
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

        private AuthorModel MapFromEntity(Author entity)
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

        public int CountAuthors()
        {
            return _context.Authors.Count();
        }
    }
}
