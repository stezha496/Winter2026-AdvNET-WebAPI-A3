using _991745453_IT_ASSET_API.Models;

namespace _991745453_IT_ASSET_API.Repositories;

public interface IUserRepository
{
    Task AddUser(User user);
    Task<List<User>> GetAllUsers();   
    Task<User?> GetUserById(int id);
    Task UpdateCurrentUser(User currentUser, User updatedUser); 
}
