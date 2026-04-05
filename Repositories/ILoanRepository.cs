using _991745453_IT_ASSET_API.Models;

namespace _991745453_IT_ASSET_API.Repositories;

public interface ILoanRepository
{
    List<Loan> GetAllLoans();
    List<Loan> GetLoansByUser(int userId);
    // Create a Loan inside this function
    Loan CheckoutAsset(Loan checkout);
    // Update Loan status
    Loan CheckinAsset(int loanId);
}
