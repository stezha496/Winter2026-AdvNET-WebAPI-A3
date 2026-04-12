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
        //context.Notifications.RemoveRange(context.Notifications);
        //context.Loans.RemoveRange(context.Loans);
        //context.Equipment.RemoveRange(context.Equipment);
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
                EmployeeId = "EMP003",
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

        if (!context.Equipment.Any())
        {
            List<Equipment> equipmentSeedData = new List<Equipment> {
                new Equipment { AssetTag = "LT-1001", DeviceName = "Dell Latitude 5420", IsAvailable = true },
                new Equipment { AssetTag = "LT-1002", DeviceName = "HP EliteBook 840", IsAvailable = false },
                new Equipment { AssetTag = "MN-2001", DeviceName = "Samsung 27\" Monitor", IsAvailable = true },
                new Equipment { AssetTag = "KB-3001", DeviceName = "Logitech MX Keys Keyboard", IsAvailable = true },
                new Equipment { AssetTag = "MS-4001", DeviceName = "Logitech MX Master 3 Mouse", IsAvailable = false },
                new Equipment { AssetTag = "DT-5001", DeviceName = "Lenovo ThinkCentre M720", IsAvailable = true },
                new Equipment { AssetTag = "TB-6001", DeviceName = "Apple iPad 9th Gen", IsAvailable = true },
                new Equipment { AssetTag = "PH-7001", DeviceName = "iPhone 13", IsAvailable = false },
                new Equipment { AssetTag = "PR-8001", DeviceName = "Brother HL-L2350DW Printer", IsAvailable = true },
                new Equipment { AssetTag = "DK-9001", DeviceName = "Dell WD19 Docking Station", IsAvailable = true }
        };
            context.Equipment.AddRange(equipmentSeedData);
            context.SaveChanges();
        }

        //to check if database is already seeded
        if (!context.Loans.Any() ||
            !context.Notifications.Any()
            )
        {
            // Populate 
            #region Loans
            // Add a test overdue loan (no ReturnDate = active, ExpectedReturnDate in the past = overdue)
            Loan testLoan = new Loan
            {
                UserId = "test-user-001",
                EquipmentId = 1,
                CheckoutDate = new DateOnly(2026, 1, 1),
                ExpectedReturnDate = new DateOnly(2026, 2, 1),  // Past date = overdue
                ReturnDate = null  // No return date = active loan
            };

            List<Loan> loanSeedData = new List<Loan> {
    // Active loans
    new Loan {
        UserId = "EMP000",
        EquipmentId = 33,
        CheckoutDate = new DateOnly(2026, 4, 1),
        ExpectedReturnDate = new DateOnly(2026, 4, 15),
        ReturnDate = null
    },
    new Loan {
        UserId = "EMP003",
        EquipmentId = 34,
        CheckoutDate = new DateOnly(2026, 4, 5),
        ExpectedReturnDate = new DateOnly(2026, 4, 20),
        ReturnDate = null
    },

    // Returned loans
    new Loan {
        UserId = "EMP000",
        EquipmentId = 35,
        CheckoutDate = new DateOnly(2026, 3, 10),
        ExpectedReturnDate = new DateOnly(2026, 3, 20),
        ReturnDate = new DateOnly(2026, 3, 18)
    },
    new Loan {
        UserId = "EMP003",
        EquipmentId = 36,
        CheckoutDate = new DateOnly(2026, 3, 1),
        ExpectedReturnDate = new DateOnly(2026, 3, 10),
        ReturnDate = new DateOnly(2026, 3, 12) // late return
    },

    // Overdue
    new Loan {
        UserId = "EMP000",
        EquipmentId = 37,
        CheckoutDate = new DateOnly(2026, 3, 25),
        ExpectedReturnDate = new DateOnly(2026, 4, 5),
        ReturnDate = null
    },

    // More variety
    new Loan {
        UserId = "EMP003",
        EquipmentId = 38,
        CheckoutDate = new DateOnly(2026, 4, 10),
        ExpectedReturnDate = new DateOnly(2026, 4, 25),
        ReturnDate = null
    },
    new Loan {
        UserId = "EMP000",
        EquipmentId = 39,
        CheckoutDate = new DateOnly(2026, 2, 15),
        ExpectedReturnDate = new DateOnly(2026, 2, 25),
        ReturnDate = new DateOnly(2026, 2, 24)
    },
    new Loan {
        UserId = "EMP003",
        EquipmentId = 40,
        CheckoutDate = new DateOnly(2026, 4, 2),
        ExpectedReturnDate = new DateOnly(2026, 4, 12),
        ReturnDate = null
    },
    new Loan {
        UserId = "EMP000",
        EquipmentId = 41,
        CheckoutDate = new DateOnly(2026, 1, 10),
        ExpectedReturnDate = new DateOnly(2026, 1, 20),
        ReturnDate = new DateOnly(2026, 1, 19)
    },
    new Loan {
        UserId = "EMP003",
        EquipmentId = 42,
        CheckoutDate = new DateOnly(2026, 4, 8),
        ExpectedReturnDate = new DateOnly(2026, 4, 18),
        ReturnDate = null
    },
    new Loan {
    UserId = "EMP000",
    EquipmentId = 33,
    CheckoutDate = null,
    ExpectedReturnDate = null,
    ReturnDate = null
},
new Loan {
    UserId = "EMP003",
    EquipmentId = 34,
    CheckoutDate = null,
    ExpectedReturnDate = null,
    ReturnDate = null
},
new Loan {
    UserId = "EMP000",
    EquipmentId = 35,
    CheckoutDate = null,
    ExpectedReturnDate = null,
    ReturnDate = null
}
};
            #endregion

            context.Loans.Add(testLoan);
            context.Loans.AddRange(loanSeedData);
            context.SaveChanges();

            Console.WriteLine("Database seeded");
        }
    }
}
