using _991745453_IT_ASSET_API.Data;
using _991745453_IT_ASSET_API.Models;
using Microsoft.EntityFrameworkCore;

namespace _991745453_IT_ASSET_API.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task AddUser(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await context.Users
            .ToListAsync();
    }

    public async Task<User?> GetUserById(int id)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task UpdateCurrentUser(User currentUser, User updatedUser)
    {
        throw new NotImplementedException();
    }
}
