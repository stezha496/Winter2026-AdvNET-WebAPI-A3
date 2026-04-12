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
public class LoansController : ControllerBase
{
    private readonly ILoanRepository _loanRepository;
    private readonly UserManager<AppUser> _userManager;

    public LoansController(ILoanRepository loanRepository, UserManager<AppUser> userManager)
    {
        _loanRepository = loanRepository;
        _userManager = userManager;
    }

    [HttpGet("my-loans")]
    public async Task<IActionResult> GetLoansByUser()
    {
        AppUser? currentUser = await _userManager.GetUserAsync(User);

        if (currentUser == null)
            return Unauthorized();

        List<Loan> loans = await _loanRepository.GetLoansByUser(currentUser.Id);
        return Ok(loans);
    }

    [Authorize(Roles = "ITAdmin")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllLoans()
    {
        List<Loan> loans = await _loanRepository.GetAllLoans();
        return Ok(loans);
    }

    [HttpPost("checkout/{equipmentId}")]
    public async Task<IActionResult> CheckoutAsset(int equipmentId)
    {
        AppUser? currentUser = await _userManager.GetUserAsync(User);

        if (currentUser == null)
            return Unauthorized();

        Loan checkout = new Loan
        {
            UserId = currentUser.Id,
            EquipmentId = equipmentId,
            CheckoutDate = DateOnly.FromDateTime(DateTime.Now)
        };

        Loan? result = await _loanRepository.CheckoutAsset(checkout);

        if (result == null)
            return BadRequest("Checkout failed.");

        return Ok(result);
    }

    [HttpPost("checkin/{loanId}")]
    public async Task<IActionResult> CheckinAsset(int loanId)
    {
        Loan? loan = await _loanRepository.CheckinAsset(loanId);

        if (loan == null)
            return NotFound();

        return Ok(loan);
    }
}