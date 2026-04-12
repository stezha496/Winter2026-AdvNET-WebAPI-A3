using _991745453_IT_ASSET_API.Models;
using _991745453_IT_ASSET_API.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _991745453_IT_ASSET_API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    // Identity
    public DbSet<AppUser> AppUsers { get; set; }
}
