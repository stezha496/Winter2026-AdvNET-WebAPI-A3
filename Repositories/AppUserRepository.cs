using _991745453_IT_ASSET_API.Data;
using _991745453_IT_ASSET_API.Models;
using _991745453_IT_ASSET_API.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace _991745453_IT_ASSET_API.Repositories;

public class AppUserRepository(AppDbContext context) : IAppUserRepository
{
    public async Task AddUser(AppUser user)
    {
        await context.AppUsers.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task<List<AppUser>> GetAllUsers()
    {
        return await context.AppUsers
            .ToListAsync();
    }

    public async Task<AppUser?> GetUserById(string id)
    {
        return await context.AppUsers.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateCurrentUser(AppUser currentUser, AppUser updatedUser)
    {
        // TODO
    }
}
