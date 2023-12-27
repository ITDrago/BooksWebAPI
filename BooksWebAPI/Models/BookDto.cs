using BooksWebAPI.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace BooksWebAPI.Models
{
    public class BookDto
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Book_DescriptionMaxSimbol]
        public string? Description { get; set; }

        public string? Author { get; set; }

    }
}
