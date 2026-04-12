using System.ComponentModel.DataAnnotations;

namespace _991745453_IT_ASSET_API.Models.DTOs;

public class LoginDTO
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Password { get; set; }
}
