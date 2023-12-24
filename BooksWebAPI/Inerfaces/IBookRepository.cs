using BooksWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BooksWebAPI.Inerfaces
{
    public interface IBookRepository
    {
        Task<ActionResult<IEnumerable<Book>>> GetAll(string userId);

        bool Add(Book book);

        Task<bool> Remove(int id);

        Task<bool> Update(int id, Book book);

        string GetCurentId(ClaimsPrincipal user);

        bool IsNull(string userId);

        bool Save();
    }
}
