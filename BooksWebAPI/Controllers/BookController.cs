using BooksWebAPI.Data;
using BooksWebAPI.Inerfaces;
using BooksWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BooksWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            var userId = _bookRepository.GetCurentId(User);
            if (userId == null)
                return BadRequest("User not found");

            var books = await _bookRepository.GetAll(userId);
            if (books == null)
                return NotFound("No books found for the user");

            return Ok(books);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            try
            {
                var userId = _bookRepository.GetCurentId(User);
                if (string.IsNullOrEmpty(userId))
                    return BadRequest("User not found");

                book.UserId = Convert.ToInt32(userId);

                if (book == null)
                    return BadRequest("Invalid book data");
                _bookRepository.Add(book);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (!await _bookRepository.Remove(id))
                return NotFound("No books found for this id");

            return Ok();
        }

        [HttpPut]

        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (!await _bookRepository.Update(id, book))
                return BadRequest("Unable to update data");

            return Ok();
        }
    }
}
