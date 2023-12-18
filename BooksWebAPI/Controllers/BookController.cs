using BooksWebAPI.Data;
using BooksWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BooksWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Book>>> GetBook()
        {
            if(_context.Books == null)
                return NotFound();
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            return await _context.Books.Where(book => Convert.ToString(book.UserId) == userId).ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult<Book>> PostWord(Book book)
        {
            try
            {

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            }
            catch (Exception ex) {return BadRequest();}

            return Ok();
        }

    }
}
