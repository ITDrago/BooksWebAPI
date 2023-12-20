using BooksWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BooksWebAPI.Inerfaces
{
    public interface IBookRepository
    {
        Task<ActionResult<IEnumerable<Book>>> GetAll(string userId);

        bool Add(Book book);

        bool Remove(Book book);

        string GetCurentId(ClaimsPrincipal user);

        bool IsNull(string userId);

        bool Save();
    }
}
