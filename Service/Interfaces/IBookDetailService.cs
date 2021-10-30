using CardManager.Models;
using CardManager.Models.ViewModels;
using CardManager.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManager.Service.Interfaces
{
    public interface IBookDetailService
    {
        BookDetailsModel GetBookDetails(int bookId);

        BookDetailsModel UpdateBookDetails(BookDetailsModel model);

        bool Create(BookDetail bookDetail);

        bool Update(BookDetail bookDetail);
    }
}
