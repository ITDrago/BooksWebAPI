using BooksWebAPI.Data;
using BooksWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
