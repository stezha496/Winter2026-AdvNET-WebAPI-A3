using _991745453_IT_ASSET_API.Models;
using _991745453_IT_ASSET_API.Models.Identity;
using _991745453_IT_ASSET_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _991745453_IT_ASSET_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    // Inject repository
    private readonly IAppUserRepository _userRepository;

    public AccountController(IAppUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("register")]
    public Task Post([FromBody] AppUser user) => _userRepository.AddUser(user);

    //[HttpPost("login")]

    //[HttpPost("logout")]

}
