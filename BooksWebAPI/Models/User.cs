using System.ComponentModel.DataAnnotations;

namespace BooksWebAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}
