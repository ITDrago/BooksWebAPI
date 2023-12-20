using BooksWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BooksWebAPI.Inerfaces
{
    public interface IBookRepository
    {
        Task<ActionResult<IEnumerable<Book>>> GetAll(string userId);

        bool Add(Book book);

        bool Remove(Book book);

        bool IsNull(string userId);

        bool Save();
    }
}
