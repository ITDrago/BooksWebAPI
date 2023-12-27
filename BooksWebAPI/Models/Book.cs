using BooksWebAPI.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace BooksWebAPI.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Book_DescriptionMaxSimbol]
        public string? Description { get; set; }

        public string? Author { get; set; }

        public DateTime CreateDate { get; set; }

        public int UserId { get; set; }
    }
}
