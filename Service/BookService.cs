﻿using CardManager.Data;
using CardManager.Models;
using CardManager.Service.Interfaces;
using CardManager.Service.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CardManager.Service
{
    public class BookService : IBookService
    {
        public readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public BookModel CreateBook(string title, string isbn, double price, int categoryId, int publisherId)
        {
            var book = new Book()
            {
                Title = title,
                ISBN = isbn,
                Price = price,
                CategoryId = categoryId,
                PublisherId = publisherId
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            return MapFromEntity(book);
        }

        public BookModel DeleteBook(int bookId)
        {
            var book = _context.Books
                .FirstOrDefault(b => b.Id == bookId);

            _context.Books.Remove(book);
            _context.SaveChanges();

            return book != null
                ? MapFromEntity(book)
                : null;
        }

        public BookModel GetBook(int bookId)
        {
            var book = _context.Books
                .Include(b => b.Publisher)
                .Include(b => b.Category)
                .FirstOrDefault(b => b.Id == bookId);

            return new BookModel()
            {
                BookId = book.Id,
                PublisherId = book.PublisherId,
                CategoryId = book.CategoryId,
                CategoryName = book.Category.Name,
                PublisherName = book.Publisher.Name,
                ISBN = book.ISBN,
                Price = book.Price,
                Title = book.Title
            }; 
        }

        public IEnumerable<BookModel> GetAll()
        {
            var bookModelList = new List<BookModel>();
            var bookList = _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .ToList();

            foreach (var model in bookList)
            {
                var bookModel = new BookModel()
                {
                    BookId = model.Id,
                    ISBN = model.ISBN,
                    Price = model.Price,
                    Title = model.Title,
                    CategoryName = model.Category?.Name,
                    PublisherName = model.Publisher?.Name,
                    AuthorList = model.BookAuthors.Select(ba => MapFromEntity(ba.Author)).ToList()
                };
                bookModelList.Add(bookModel);
            }
            return bookModelList;
        }

        public BookModel UpdateBook(BookModel model)
        {
            var book = _context.Books
                .SingleOrDefault(b => b.Id == model.BookId);

            book.ISBN = model.ISBN;
            book.Price = model.Price;
            book.Title = model.Title;

            _context.Books.Update(book);
            _context.SaveChanges();

            return model;
        }

        private BookModel MapFromEntity(Book entity)
        {
            return new BookModel()
            {
                BookId = entity.Id,
                PublisherId = entity.PublisherId,
                CategoryId = entity.CategoryId,
                ISBN = entity.ISBN,
                Price = entity.Price,
                Title = entity.Title
            };
        }

        public int CountBooks()
        {
            return _context.Books.Count();
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
    }
}
