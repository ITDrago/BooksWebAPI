using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksWebAPI.Data;
using BooksWebAPI.Models;
using BooksWebAPI.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BooksWebAPI.Test.Repository
{
    public class BookRepositroy
    {
        private async Task<ApplicationDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Books.CountAsync() <= 0)
            {
                for (int i = 1; i < 5; i++)
                {
                    databaseContext.Books.Add(
                        new Book()
                        {
                            Title = "TestTest",
                            Description = "Simple",
                            Author = "Martin3",
                            CreateDate = DateTime.Now,
                            UserId = 1
                        });
                }
                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }

        [Fact]
        public async Task BookPokemonRepository_GetBooks_ReturnsBooks()
        {
            //Arrange 
            var userId = "1";
            var dbContext = await GetDatabaseContext();
            var bookRepository = new BookRepository(dbContext);

            //Act
            var result = await bookRepository.GetAll(userId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<IEnumerable<Book>>>();

        }
    }
}
