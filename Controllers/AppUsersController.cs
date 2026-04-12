using _991745453_IT_ASSET_API.Models;
using _991745453_IT_ASSET_API.Models.Identity;
using _991745453_IT_ASSET_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _991745453_IT_ASSET_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppUsersController : ControllerBase
{
    // Inject repository
    private readonly IAppUserRepository _userRepository;
    public AppUsersController(IAppUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public List<AppUser> GetAllUsers()
    {
        return _userRepository.GetAllUsers().Result;
    }

    // Get currently logged in user
    [HttpGet("me")]
    public AppUser GetCurrentUser(string userId)
    {
        return _userRepository.GetUserById(userId).Result;
    }

    // TODO
    // Can only have 1 parameter?
    // Updates password, phone number
    //[HttpPut("update")]
    //public void UpdateCurrentUser(User currentUser, User updatedUser)
    //{
    //    _userRepository.UpdateCurrentUser(currentUser, updatedUser);
    //}
}
