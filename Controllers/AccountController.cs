using _991745453_IT_ASSET_API.Models;
using _991745453_IT_ASSET_API.Models.DTOs;
using _991745453_IT_ASSET_API.Models.Identity;
using _991745453_IT_ASSET_API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _991745453_IT_ASSET_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(
        UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        AppUser newUser = new AppUser
        {
            UserName = dto.FirstName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            EmployeeId = dto.EmployeeId,
            Department = dto.Department
        };

        IdentityResult result = await _userManager.CreateAsync(newUser, dto.Password!);

        if (!result.Succeeded)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
            return BadRequest(ModelState);
        }

        return Ok(newUser);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        AppUser? user = await _userManager.FindByNameAsync(dto.UserName!);

        if (user == null)
            return Unauthorized("Invalid username or password.");

        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, dto.Password!, false, false);

        if (!result.Succeeded)
            return Unauthorized("Invalid username or password.");

        return Ok(user);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok("Logged out successfully.");
    }

}
