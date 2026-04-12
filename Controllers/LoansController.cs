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

    public LoansController(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    [HttpGet("my-loans")]
    public List<Loan> GetLoansByUser(string userId)
    {
        return _loanRepository.GetLoansByUser(userId).Result;
    }

    [HttpGet("all")]
    public List<Loan> GetAllLoans()
    {
        return _loanRepository.GetAllLoans().Result;
    }

    [HttpPost("api/checkout/{equipmentId}")]
    public Loan CheckoutAsset(int equipmentId)
    {
        Loan checkout = new Loan();

        return _loanRepository.CheckoutAsset(checkout).Result;
    }

    [HttpPost("api/checkin/{loanId}")]
    public Loan CheckinAsset(int loanId)
    {
        return _loanRepository.CheckinAsset(loanId).Result;
    }
}
