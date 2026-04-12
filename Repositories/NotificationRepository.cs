using _991745453_IT_ASSET_API.Data;
using _991745453_IT_ASSET_API.Models;
using Microsoft.EntityFrameworkCore;

namespace _991745453_IT_ASSET_API.Repositories;

public class NotificationRepository(AppDbContext context) : INotificationRepository
{
    public async Task<List<Notification>> GetNotificationsByUser(string userId)
    {
        return await context.Notifications
            .Where(n => n.UserId == userId)
            .ToListAsync();
    }

    public async Task<Notification?> UpdateNotificationRead(int notificationId)
    {
        Notification? toUpdate = await context.Notifications
            .FirstOrDefaultAsync(x => x.NotificationId == notificationId);

        if (toUpdate != null)
        {
            toUpdate.IsRead = !toUpdate.IsRead;
            await context.SaveChangesAsync();
        }
        return toUpdate;
    }

    public async Task SaveNotificationAsync(Notification notification)
    {
        await context.Notifications.AddAsync(notification);
        await context.SaveChangesAsync();
    }

    // Checks if notification exists for a specific loan. To avoid duplicates
    public async Task<bool> NotificationExistsForLoan(int loanId)
    {
        return await context.Notifications.AnyAsync(n => n.LoanId == loanId);
    }
}
