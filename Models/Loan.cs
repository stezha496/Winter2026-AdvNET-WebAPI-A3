namespace _991745453_IT_ASSET_API.Models;

public class Loan
{
    public int Id { get; set; }
    // This will match AppUser.EmployeeId
    public string? UserId { get; set; }
    public int? EquipmentId { get; set; }
    public DateOnly? CheckoutDate { get; set; }
    public DateOnly? ReturnDate { get; set; }
    public DateOnly? ExpectedReturnDate { get; set; }
}
