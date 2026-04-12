using _991745453_IT_ASSET_API.Data;
using _991745453_IT_ASSET_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace _991745453_IT_ASSET_API.Repositories;

public class LoanRepository(AppDbContext context) : ILoanRepository
{
    public async Task<Loan?> CheckoutAsset(Loan loan)
    {
        // Check if equipment is available
        Equipment? equipment = await context.Equipment.FirstOrDefaultAsync(e => e.Id == loan.EquipmentId);

        if (equipment == null || !equipment.IsAvailable)
            return null;

        // Set expected return date to 2 weeks from checkout
        loan.CheckoutDate = DateOnly.FromDateTime(DateTime.Now);
        loan.ExpectedReturnDate = DateOnly.FromDateTime(DateTime.Now.AddDays(14));

        // Mark equipment as unavailable
        equipment.IsAvailable = false;

        await context.Loans.AddAsync(loan);
        await context.SaveChangesAsync();

        return loan;
    }

    // Updates loan and equipment
    public async Task<Loan?> CheckinAsset(int loanId)
    {
        Loan? loan = await context.Loans.FirstOrDefaultAsync(l => l.Id == loanId);

        if (loan == null)
            return null;

        // Set the return date to today
        loan.ReturnDate = DateOnly.FromDateTime(DateTime.Now);

        // Mark equipment as available again
        Equipment? equipment = await context.Equipment.FirstOrDefaultAsync(e => e.Id == loan.EquipmentId);
        if (equipment != null)
            equipment.IsAvailable = true;

        await context.SaveChangesAsync();

        return loan;
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
