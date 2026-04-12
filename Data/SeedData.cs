using _991745453_IT_ASSET_API.Models;
using _991745453_IT_ASSET_API.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace _991745453_IT_ASSET_API.Data;

public class SeedData
{
    public static async Task SeedDatabase(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // Inject user and role managers
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        //Run any migrations automatically 
        context.Database.Migrate();

        // Clear existing data
        context.Notifications.RemoveRange(context.Notifications);
        context.Loans.RemoveRange(context.Loans);
        context.Equipment.RemoveRange(context.Equipment);
        context.SaveChanges();

        #region UsersSeed

        // Seed Employee role
        if (!await roleManager.RoleExistsAsync("Employee"))
            await roleManager.CreateAsync(new IdentityRole("Employee"));

        // Seed test Employee user
        if (await userManager.FindByNameAsync("TestEmployee") == null)
        {
            AppUser employeeUser = new AppUser
            {
                UserName = "TestEmployee",
                EmployeeId = "EMP001",
                Department = "HR"
            };

            IdentityResult result = await userManager.CreateAsync(employeeUser, "Employee1234!");
            if (result.Succeeded)
                await userManager.AddToRoleAsync(employeeUser, "Employee");
        }

        // Seed ITAdmin role
        if (!await roleManager.RoleExistsAsync("ITAdmin"))
            await roleManager.CreateAsync(new IdentityRole("ITAdmin"));

        // Seed ITAdmin user
        if (await userManager.FindByNameAsync("ITAdmin") == null)
        {
            AppUser adminUser = new AppUser
            {
                UserName = "ITAdmin",
                EmployeeId = "EMP000",
                Department = "IT"
            };

            IdentityResult result = await userManager.CreateAsync(adminUser, "Admin1234!");
            if (result.Succeeded)
                await userManager.AddToRoleAsync(adminUser, "ITAdmin");
        }
        #endregion

        //to check if database is already seeded
        if (!context.Loans.Any() ||
            !context.Notifications.Any() ||
            !context.Equipment.Any()
            )
        {
            // Populate 
            // Add a test overdue loan (no ReturnDate = active, ExpectedReturnDate in the past = overdue)
            Loan testLoan = new Loan
            {
                UserId = "test-user-001",
                EquipmentId = 1,
                CheckoutDate = new DateOnly(2026, 1, 1),
                ExpectedReturnDate = new DateOnly(2026, 2, 1),  // Past date = overdue
                ReturnDate = null  // No return date = active loan
            };

            context.Loans.Add(testLoan);
            context.SaveChanges();

            Console.WriteLine("Database seeded");
        }
    }
}
