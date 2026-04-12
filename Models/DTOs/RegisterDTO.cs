using System.ComponentModel.DataAnnotations;

namespace _991745453_IT_ASSET_API.Models.DTOs;

public class RegisterDTO
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? EmployeeId { get; set; }

    [Required]
    public string? Department { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [Phone]
    public string? PhoneNumber { get; set; }

    [Required]
    public string? Password { get; set; }
}
