using _991745453_IT_ASSET_API.Models;
using _991745453_IT_ASSET_API.Models.Identity;
using _991745453_IT_ASSET_API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _991745453_IT_ASSET_API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    // Inject repository
    private readonly ILoanRepository _loanRepository;
    private readonly INotificationRepository _notificationRepository;

    private UserManager<AppUser> userManager;

    public NotificationsController(
        ILoanRepository loanRepository, 
        INotificationRepository notificationRepository,
        UserManager<AppUser> userManager
        )
    {
        _loanRepository = loanRepository;
        _notificationRepository = notificationRepository;
        this.userManager = userManager;
    }

    // This is a working example of using the userManager
    [AllowAnonymous]
    [HttpPost("test")]
    public async Task<IActionResult> CreateTestUser()
    {
        if (ModelState.IsValid)
        {
            AppUser testUser = new AppUser
            {
                UserName = "TestUserName",
                EmployeeId = "EMP001",
                Department = "IT"
            };

            IdentityResult result = await userManager.CreateAsync(testUser, "TestPassword1!");
            if (result.Succeeded)
            {
                return Ok(testUser);
            }
            else
            {
                Errors(result);
            }
        }
        return BadRequest(ModelState);
    }

    // When the /generate-overdue endpoint is called, compare the ExpectedReturnDate of active
    // loans with the current date(DateTime.Now).
    // If overdue, generate a notification and store it in the Notifications table.
    // Prevent duplicate notifications by checking if one already exists for the same active loan.
    [Authorize(Roles = "ITAdmin")]
    [HttpPost("generate-overdue")]
    public async Task<IActionResult> GenerateOverdueNotification()
    {
        List<Notification> notifications = new List<Notification>();

        // Get all active loans (no return date)
        List<Loan> loans = await _loanRepository.GetAllLoans();
        List<Loan> activeLoans = loans.Where(x => x.ReturnDate == null).ToList();

        DateOnly today = DateOnly.FromDateTime(DateTime.Now);

        foreach (Loan loan in activeLoans)
        {
            // Skip loans with no expected return date
            if (loan.ExpectedReturnDate == null) {
                continue;
            }

            // Check if overdue
            if (loan.ExpectedReturnDate < today)
            {
                // Prevent duplicate notifications for the same loan
                bool exists = await _notificationRepository.NotificationExistsForLoan(loan.Id);
                if (exists)
                {
                    continue;
                }

                Notification notification = new Notification
                {
                    UserId = loan.UserId,
                    Message = $"Overdue loan notice: Equipment ID {loan.EquipmentId} was expected to be returned on {loan.ExpectedReturnDate}.",
                    IsRead = false,
                    CreatedAt = today
                };

                await _notificationRepository.SaveNotificationAsync(notification);
                notifications.Add(notification);
            }
        }
        return Ok(notifications);
    }

    [HttpGet("my-alerts")]
    public async Task<IActionResult> GetCurrentUserAlerts()
    {
        AppUser? currentUser = await userManager.GetUserAsync(User);

        if (currentUser == null)
        {
            return Unauthorized();
        }

        List<Notification> notifications = await _notificationRepository.GetNotificationsByUser(currentUser.Id);

        return Ok(notifications);
    }

    // Update the Read property of a given NotificationId
    [HttpPut("{id}/read")]
    public async Task<IActionResult> UpdateNotificationRead(int id) {
        Notification? notification = await _notificationRepository.UpdateNotificationRead(id);

        if (notification == null)
        {
            return NotFound();
        }

        return Ok(notification);
    }

    // For Error handling
    void Errors(IdentityResult result)
    {
        foreach (IdentityError error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }
    }
}
