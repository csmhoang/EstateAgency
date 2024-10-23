using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class chageRoomAndPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "1a015dd1-0091-4e7f-be98-3c62775a9c7a");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "4a8b18f4-be0f-4251-9e38-5aaf2f878af6");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "c6502487-5ebb-43e9-a9c8-a3b7c215baf4");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Posts");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Rooms",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2778f566-a483-4f82-8a16-a038aeae3c97", "2eaf222d-1086-45c5-8eff-2f425c42d358", "landlord", "LANDLORD" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6f9474f1-4dd8-4540-8256-0a310ab27237", "9de0edc2-c9c0-4466-bdda-7fa1496280ec", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e8154d38-17d3-45cd-9569-884ba98ca5d0", "6c44a95a-6b6a-41b6-bd8d-2448e7ae4cae", "tenant", "TENANT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "2778f566-a483-4f82-8a16-a038aeae3c97");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f9474f1-4dd8-4540-8256-0a310ab27237");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e8154d38-17d3-45cd-9569-884ba98ca5d0");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "Condition",
                table: "Rooms",
                newName: "ConditionRoom");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Reservations",
                newName: "StatusInvoice");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Posts",
                newName: "StatusInvoice");

            migrationBuilder.RenameColumn(
                name: "IsAccept",
                table: "Posts",
                newName: "IsAcceptPost");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Payments",
                newName: "StatusInvoice");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "MaintenanceRequests",
                newName: "StatusInvoice");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Leases",
                newName: "StatusInvoice");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Invoices",
                newName: "StatusInvoice");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Posts",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1a015dd1-0091-4e7f-be98-3c62775a9c7a", "225c14a5-f200-495d-a9eb-bff10ab7e6a4", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4a8b18f4-be0f-4251-9e38-5aaf2f878af6", "4b10484d-b12b-4a1c-a33c-a09a199f85a9", "landlord", "LANDLORD" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c6502487-5ebb-43e9-a9c8-a3b7c215baf4", "064d4c01-d698-4d2e-bc1a-b3410869f005", "tenant", "TENANT" });
        }
    }
}
