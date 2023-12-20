using BooksWebAPI.Data;
using BooksWebAPI.Inerfaces;
using BooksWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BooksWebAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
                _context = context;
        }

        public bool Add(Book book)
        {
            _context.Books.Add(book);
            return Save();
        }

        public async Task<ActionResult<IEnumerable<Book>>> GetAll(string userId)
        {
            return await _context.Books.Where(book => Convert.ToString(book.UserId) == userId).ToListAsync();
        }

        public bool Remove(Book book)
        {
            _context.Books.Remove(book);
            return Save();
        }

        public bool IsNull(string userId)
        {
            if(_context.Books.Where(book => Convert.ToString(book.UserId) == userId).ToListAsync() == null)
                return true;
            return false;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
