
using _991745453_IT_ASSET_API.Data;
using _991745453_IT_ASSET_API.Models.Identity;
using _991745453_IT_ASSET_API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace _991745453_IT_ASSET_API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        //builder.Services.AddOpenApi();

        //Register the DbContext
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EPConn")));

        // Add Identity
        builder.Services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        // Configure Cookie Authentication
        builder.Services.ConfigureApplicationCookie(options =>
        {
            // Where to redirect if not logged in
            options.LoginPath = "/api/account/login";

            // Where to redirect if logged in but wrong role
            options.AccessDeniedPath = "/api/account/access-denied";

            // How long the cookie lasts
            options.ExpireTimeSpan = TimeSpan.FromHours(1);

            // Cookie name in the browser
            options.Cookie.Name = "YourAppCookie";

            // Refresh expiry on activity
            options.SlidingExpiration = true;

            // Prevent JavaScript from accessing the cookie (security)
            options.Cookie.HttpOnly = true;

            // Only send cookie over HTTPS
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });

        // Add repos
        builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
        builder.Services.AddScoped<ILoanRepository, LoanRepository>();
        builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
        builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();

        // Swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Seed database with test data
        SeedData.SeedDatabase(app);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            //app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
