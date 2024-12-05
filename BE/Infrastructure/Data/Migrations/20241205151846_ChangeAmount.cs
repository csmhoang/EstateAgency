using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class ChangeAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leases_Users_TenantId",
                table: "Leases");

            migrationBuilder.DropIndex(
                name: "IX_Leases_TenantId",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Leases");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Bookings");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Leases",
                type: "nvarchar(265)",
                maxLength: 265,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "Leases",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leases_TenantId",
                table: "Leases",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_Users_TenantId",
                table: "Leases",
                column: "TenantId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
