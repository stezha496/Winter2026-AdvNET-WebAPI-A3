using _991745453_IT_ASSET_API.Models;
using _991745453_IT_ASSET_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _991745453_IT_ASSET_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    // Inject repository
    private readonly IUserRepository _userRepository;
    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public List<User> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    // Get currently logged in user
    [HttpGet("me")]
    public User GetCurrentUser(int userId)
    {
        return _userRepository.GetUserById(userId);
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
