using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace _991745453_IT_ASSET_API.Models.Identity;

public class AppUser : IdentityUser
{
    [Required]
    public string? EmployeeId { get; set; }
    [Required]
    public string? Department { get; set; }
}
