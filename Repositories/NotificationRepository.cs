using _991745453_IT_ASSET_API.Data;
using _991745453_IT_ASSET_API.Models;
using Microsoft.EntityFrameworkCore;

namespace _991745453_IT_ASSET_API.Repositories;

public class NotificationRepository(AppDbContext context) : INotificationRepository
{
    public async Task<List<Notification>> GetNotificationsByUser(string userId)
    {
        return await context.Notifications
            .Where(x => x.UserId == userId)
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
}
