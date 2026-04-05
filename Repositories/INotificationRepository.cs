using _991745453_IT_ASSET_API.Models;

namespace _991745453_IT_ASSET_API.Repositories;

public interface INotificationRepository
{
    List<Notification> GetNotificationsByUser(int userId);

    Notification UpdateNotificationRead(int notificationId);
}
