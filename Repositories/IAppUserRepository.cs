using _991745453_IT_ASSET_API.Models.Identity;

namespace _991745453_IT_ASSET_API.Repositories;

public interface IAppUserRepository
{
    Task AddUser(AppUser user);
    Task<List<AppUser>> GetAllUsers();   
    Task<AppUser?> GetUserById(string id);
    Task UpdateCurrentUser(AppUser currentUser, AppUser updatedUser); 
}
