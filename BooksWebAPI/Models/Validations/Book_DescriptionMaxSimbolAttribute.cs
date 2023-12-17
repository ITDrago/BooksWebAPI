using System.ComponentModel.DataAnnotations;

namespace BooksWebAPI.Models.Validations
{
    public class Book_DescriptionMaxSimbolAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var book = validationContext.ObjectInstance as Book;

            if (book != null && !string.IsNullOrWhiteSpace(book.Description))
            {
                if (book.Description.Length > 200)
                    return new ValidationResult("Book description length has to be less than or equal to 200 characters");
            }

            return ValidationResult.Success;
        }
    }
}
