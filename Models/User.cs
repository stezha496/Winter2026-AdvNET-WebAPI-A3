using System.ComponentModel.DataAnnotations;

namespace _991745453_IT_ASSET_API.Models;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter your name")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "Please enter your name")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "Please enter your email")]
    [EmailAddress]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Please enter your phone number")]
    [RegularExpression(@"^\d{3}-\d{3}-\d{4}$",
        ErrorMessage = "Phone number must be in the format 999-999-9999")]
    public required string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Please enter your password")]
    public required string Password { get; set; }

    //public List<Book> BorrowedBooks { get; set; } = [];
}
