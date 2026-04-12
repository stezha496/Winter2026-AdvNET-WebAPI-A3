using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _991745453_IT_ASSET_API.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationLoanId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoanId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoanId",
                table: "Notifications");
        }
    }
}
