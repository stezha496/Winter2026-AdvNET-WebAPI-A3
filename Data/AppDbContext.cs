using _991745453_IT_ASSET_API.Models;
using Microsoft.EntityFrameworkCore;

namespace _991745453_IT_ASSET_API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<User> Users { get; set; }
}
