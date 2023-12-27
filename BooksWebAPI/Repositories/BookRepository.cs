using AutoMapper;
using BooksWebAPI.Data;
using BooksWebAPI.Inerfaces;
using BooksWebAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BooksWebAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        public BookRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper; 
        }

        public Task<bool> Add(Book book)
        {
            _context.Books.Add(book);
            return Save();
        }

        public  async Task<IEnumerable<BookDto>> GetAll(string userId)
        {
            var books =  await _context.Books.Where(book => Convert.ToString(book.UserId) == userId).ToListAsync();
            var booksDto =  _mapper.Map<IEnumerable<BookDto>>(books);
            return booksDto;
        }

        public async Task<bool> Remove(int id)
        {
            var book =  await _context.Books.FirstOrDefaultAsync(book => book.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                return await  Save();
            }
            return false;
        }

        public async Task<bool> Update(int id, Book book)
        {
            if (id != book.Id)
                return false;

            try
            {
                _context.Entry(book).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;
        }

        public bool IsNull(string userId)
        {
            if(_context.Books.Where(book => Convert.ToString(book.UserId) == userId).ToListAsync() == null)
                return true;
            return false;
        }

        public string? GetCurentId(ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
