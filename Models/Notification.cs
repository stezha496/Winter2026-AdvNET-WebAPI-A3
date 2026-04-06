namespace _991745453_IT_ASSET_API.Models;

public class Notification
{
    public int NotificationId { get; set; }
    public string? UserId { get; set; }
    public string? Message { get; set; }
    public bool IsRead { get; set; }
    public DateOnly CreatedAt { get; set; } // Using DateOnly instead of DateTime for easier formatting
}
