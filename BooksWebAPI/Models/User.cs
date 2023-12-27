using System.ComponentModel.DataAnnotations;

namespace BooksWebAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string PasswordHash { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}
