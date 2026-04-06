using _991745453_IT_ASSET_API.Models;
using _991745453_IT_ASSET_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _991745453_IT_ASSET_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    // Inject repository
    private readonly IUserRepository _userRepository;

    public AccountController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("register")]
    public User Post([FromBody] User user) => _userRepository.AddUser(user);

    //[HttpPost("login")]

    //[HttpPost("logout")]

}
