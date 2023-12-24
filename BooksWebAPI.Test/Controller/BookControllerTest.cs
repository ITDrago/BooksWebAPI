using BooksWebAPI.Controllers;
using BooksWebAPI.Inerfaces;
using BooksWebAPI.Models;
using BooksWebAPI.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace BooksWebAPI.Test.Controller
{
    public class BookControllerTest
    {
        //Dependencies
        private readonly IBookRepository _bookRepositroy;

        public BookControllerTest()
        {
            _bookRepositroy = A.Fake<IBookRepository>();
        }

        [Fact]
        public async Task BookController_GetBooks_ReturnOKAsync()
        {
            //Arrange
            var books = A.Fake<ICollection<Book>>();
            string userId = "1";
            var controller = new BookController(_bookRepositroy);

            //Act
            var result = await controller.GetBooks();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<IEnumerable<Book>>>();
            
        }

        [Fact]
        public async Task BookController_PostBook_ReturnOkAsync()
        {
            //Arange
            var book = A.Fake<Book>();
            string userId = "1";
            var controller = new BookController(_bookRepositroy);

            //Act
            var result = await controller.PostBook(book);

            //Assert
            result.Should().BeOfType<ActionResult<Book>>();

        }

        [Fact]
        public async Task BookController_DeleteBook_ReturnOkAsync()
        {
            //Arange
            int bookId = 8;
            string userId = "1";
            var controller = new BookController(_bookRepositroy);

            //Act
            var result = await controller.DeleteBook(bookId);

            //Assrt
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task BookController_PutBook_ReturnOkAsync()
        {
            //Arange
            int bookId = 1;
            var book = A.Fake<Book>();

            var controller = new BookController(_bookRepositroy);

            //Act
            var result = await controller.PutBook(bookId,book);

            //Assrt
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));



        }

    }

   
}
