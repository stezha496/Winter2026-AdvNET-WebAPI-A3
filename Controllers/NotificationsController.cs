using _991745453_IT_ASSET_API.Models;
using _991745453_IT_ASSET_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _991745453_IT_ASSET_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    // Inject repository
    private readonly ILoanRepository _loanRepository;
    private readonly INotificationRepository _notificationRepository;

    [HttpPost("generate-overdue")]
    public List<Notification> GenerateOverdueNotification()
    {
        List<Notification> notifications = new List<Notification>();

        // Get all active loans for current User
        List<Loan> activeLoans = _loanRepository.GetLoansByUser();
        // Get all overdue loans
        List<Loan> overdueLoans;

        // If list is null or empty, nothing overdue
        if(activeLoans.Count == 0 || activeLoans is null)
        {
            return [];
        }

        // TODO
        overdueLoans = activeLoans.ToList();

        // For each loan that is overdue, add it to notification
        foreach (Loan loan in overdueLoans)
        {
            // TODO
            // Check if is overdue. 
            if ()
            {
                // TODO
                Notification newNotification = new Notification();
                // Add to overdue notification
                notifications.Add(newNotification);
            }
        }
        return notifications;
    }

    [HttpGet("my-alerts")]
    public List<Notification> GetCurrentUserAlerts()
    {
        return _notificationRepository.GetNotificationsByUser();
    }

    [HttpPut("{id}/read")]
    public Notification UpdateNotificationRead(int notificationId) { 
        return _notificationRepository.UpdateNotificationRead(notificationId);
    }
}
