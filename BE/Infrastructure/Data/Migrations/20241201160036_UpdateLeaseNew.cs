using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class UpdateLeaseNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leases_Rooms_RoomId",
                table: "Leases");

            migrationBuilder.DropIndex(
                name: "IX_Leases_RoomId",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Leases");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoomId",
                table: "Leases",
                type: "nvarchar(36)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leases_RoomId",
                table: "Leases",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_Rooms_RoomId",
                table: "Leases",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }
    }
}
