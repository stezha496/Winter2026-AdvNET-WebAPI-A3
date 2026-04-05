using _991745453_IT_ASSET_API.Models;
using _991745453_IT_ASSET_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _991745453_IT_ASSET_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    // Inject repository
    private readonly ILoanRepository _loanRepository;

    [HttpGet("my-loans")]
    public List<Loan> GetLoansByUser(int userId)
    {
        return _loanRepository.GetLoansByUser(userId);
    }

    [HttpGet("all")]
    public List<Loan> GetAllLoans()
    {
        return _loanRepository.GetAllLoans();
    }

    [HttpPost("api/checkout/{equipmentId}")]
    public Loan CheckoutAsset(int equipmentId)
    {
        Loan checkout = new Loan();

        return _loanRepository.CheckoutAsset(checkout);
    }

    [HttpPost("api/checkin/{loanId}")]
    public Loan CheckinAsset(int loanId)
    {
        return _loanRepository.CheckinAsset(loanId);
    }
}
