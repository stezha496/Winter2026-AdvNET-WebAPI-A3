using _991745453_IT_ASSET_API.Data;
using _991745453_IT_ASSET_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace _991745453_IT_ASSET_API.Repositories;

public class LoanRepository(AppDbContext context) : ILoanRepository
{
    public Task<Loan> CheckinAsset(int loanId)
    {
        throw new NotImplementedException();
    }

    public Task<Loan> CheckoutAsset(Loan checkout)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Loan>> GetAllLoans()
    {
        return await context.Loans
            .ToListAsync();
    }

    public async Task<List<Loan>> GetLoansByUser(string userId)
    {
        return await context.Loans
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }
}
