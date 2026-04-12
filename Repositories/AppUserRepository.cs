using _991745453_IT_ASSET_API.Data;
using _991745453_IT_ASSET_API.Models;
using _991745453_IT_ASSET_API.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace _991745453_IT_ASSET_API.Repositories;

public class AppUserRepository : IAppUserRepository
{
    private readonly AppDbContext context;
    private readonly UserManager<AppUser> userManager;

    public AppUserRepository(AppDbContext context, UserManager<AppUser> userManager)
    {
        this.context = context;
        this.userManager = userManager;
    }

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

    public async Task<List<string>> UpdateGivenUser(AppUser user, string password, string? phoneNumber)
    {
        List<string> errors = new List<string>();

        // Update phone number
        user.PhoneNumber = phoneNumber;
        IdentityResult updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            errors.AddRange(updateResult.Errors.Select(e => e.Description));

        // Update password
        string resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
        IdentityResult passwordResult = await userManager.ResetPasswordAsync(user, resetToken, password);
        if (!passwordResult.Succeeded)
            errors.AddRange(passwordResult.Errors.Select(e => e.Description));

        return errors;
    }
}
