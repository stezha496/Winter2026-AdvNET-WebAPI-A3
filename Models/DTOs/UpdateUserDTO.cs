using System.ComponentModel.DataAnnotations;

namespace _991745453_IT_ASSET_API.Models.DTOs;

public class UpdateUserDTO
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    [Phone]
    public string? PhoneNumber { get; set; }
}