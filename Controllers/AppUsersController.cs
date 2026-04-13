using _991745453_IT_ASSET_API.Models;
using _991745453_IT_ASSET_API.Models.DTOs;
using _991745453_IT_ASSET_API.Models.Identity;
using _991745453_IT_ASSET_API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _991745453_IT_ASSET_API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AppUsersController : ControllerBase
{
    // Inject repository
    private readonly IAppUserRepository _userRepository;
    private UserManager<AppUser> userManager;

    public AppUsersController(
        IAppUserRepository userRepository,
        UserManager<AppUser> userManager
        )
    {
        _userRepository = userRepository;
        this.userManager = userManager;
    }

    [Authorize(Roles = "ITAdmin")]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        List<AppUser> allUsers = await _userRepository.GetAllUsers();
        return Ok(allUsers);
    }

    // Get currently logged in user
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        AppUser? currentUser = await userManager.GetUserAsync(User);

        if (currentUser == null)
        {
            return Unauthorized();
        }

        return Ok(currentUser);
    }

    // Updates password, phone number
    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO dto)
    {
        AppUser? currentUser = await userManager.FindByNameAsync(dto.UserName!);

        if (currentUser == null)
        {
            return NotFound();
        }

        List<string> errors = await _userRepository.UpdateGivenUser(currentUser, dto.Password!, dto.PhoneNumber);

        if (errors.Any())
        {
            return BadRequest(errors);
        }

        return Ok(currentUser);
    }

    // Gets user role (identity)

    [HttpGet("{id}/role")]
    public async Task<IActionResult> GetUserRole(string id)
    {
        AppUser? user = await userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        IList<string> roles = await userManager.GetRolesAsync(user);
        string role = roles.FirstOrDefault() ?? "Employee";

        //Console.WriteLine($"Role for user {id}: {role}");
        return Ok(role);
    }
}
