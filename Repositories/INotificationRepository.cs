using _991745453_IT_ASSET_API.Models;

namespace _991745453_IT_ASSET_API.Repositories;

public interface INotificationRepository
{
    Task<List<Notification>> GetNotificationsByUser(string userId);

    Task<Notification?> UpdateNotificationRead(int notificationId);
}
