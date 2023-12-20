using BooksWebAPI.Data;
using BooksWebAPI.Inerfaces;
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
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
  
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if(userId == null)
                return BadRequest("User not found");

            var books = await _bookRepository.GetAll(userId);
            if (books == null)
                return NotFound("No books found for the user");

            return Ok(books);
           

        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostWord(Book book)
        {
            if (book == null)
                return BadRequest("Invalid book data");

            try
            { 
                _bookRepository.Add(book);
                return Ok();
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); }

            
        }

    }
}
