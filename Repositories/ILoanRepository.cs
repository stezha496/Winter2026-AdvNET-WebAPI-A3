using _991745453_IT_ASSET_API.Models;

namespace _991745453_IT_ASSET_API.Repositories;

public interface ILoanRepository
{
    Task<List<Loan>> GetAllLoans();
    Task<List<Loan>> GetLoansByUser(string userId);
    // Create a Loan inside this function
    Task<Loan> CheckoutAsset(Loan checkout);
    // Update Loan status
    Task<Loan> CheckinAsset(int loanId);
}
