using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class changeMaintenanceRequestCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Leases_LeaseId",
                table: "Invoices");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08b68b45-2728-478e-b9ea-ebade45456c2");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "23f8c185-9524-43e2-8b29-f824f2b0af98");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "61c9ec79-48a6-483b-8e3b-d9ff58447412");

            migrationBuilder.RenameColumn(
                name: "RequestCode",
                table: "MaintenanceRequests",
                newName: "MaintenanceRequestCode");

            migrationBuilder.AlterColumn<string>(
                name: "LeaseId",
                table: "Invoices",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Leases_LeaseId",
                table: "Invoices",
                column: "LeaseId",
                principalTable: "Leases",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Leases_LeaseId",
                table: "Invoices");

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

            migrationBuilder.RenameColumn(
                name: "MaintenanceRequestCode",
                table: "MaintenanceRequests",
                newName: "RequestCode");

            migrationBuilder.AlterColumn<string>(
                name: "LeaseId",
                table: "Invoices",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "08b68b45-2728-478e-b9ea-ebade45456c2", "d9d686d8-1064-416f-8483-cdfe18054d2c", "landlord", "LANDLORD" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "23f8c185-9524-43e2-8b29-f824f2b0af98", "6802c89e-3429-4b4c-b38b-82e18a89871e", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "61c9ec79-48a6-483b-8e3b-d9ff58447412", "2ec15d4f-472f-4802-b178-a50ff4358d6a", "tenant", "TENANT" });

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Leases_LeaseId",
                table: "Invoices",
                column: "LeaseId",
                principalTable: "Leases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
