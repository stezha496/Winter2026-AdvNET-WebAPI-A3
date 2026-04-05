using _991745453_IT_ASSET_API.Models;

namespace _991745453_IT_ASSET_API.Repositories;

public interface IUserRepository
{
    User AddUser(User user);
    List<User> GetAllUsers();   
    User GetUserById(int id);
    void UpdateCurrentUser(User currentUser, User updatedUser); 
}
