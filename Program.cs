
using _991745453_IT_ASSET_API.Data;
using _991745453_IT_ASSET_API.Repositories;
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

        // Add repos
        builder.Services.AddSingleton<IEquipmentRepository, EquipmentRepository>();
        builder.Services.AddSingleton<ILoanRepository, LoanRepository>();
        builder.Services.AddSingleton<INotificationRepository, NotificationRepository>();
        builder.Services.AddSingleton<IUserRepository, UserRepository>();

        // Swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

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
