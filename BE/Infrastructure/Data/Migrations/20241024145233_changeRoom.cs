using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class changeRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3ae296f8-1fa2-4cbf-9a67-a0bf3f9060c4");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "780467e7-e088-4922-ba7e-f4e114ae98e0");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d865fd8c-e8ef-4cfe-9f75-8ae3ba316917");

            migrationBuilder.AlterColumn<decimal>(
                name: "Area",
                table: "Rooms",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Interior",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Toilet",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6d43f94b-bff3-4847-9609-113d4ad07888", "e27c3fb9-8407-4d3e-84cc-80d3735b57b7", "landlord", "LANDLORD" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8cc53284-20f0-4ccf-8781-7da021c193db", "3c639584-11b9-4c10-b21f-06c182c0f483", "tenant", "TENANT" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cae1c6ac-9fc5-47e1-86e3-b616647aa000", "0555f2ce-85b2-457c-99b9-182e638e107b", "admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6d43f94b-bff3-4847-9609-113d4ad07888");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8cc53284-20f0-4ccf-8781-7da021c193db");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "cae1c6ac-9fc5-47e1-86e3-b616647aa000");

            migrationBuilder.DropColumn(
                name: "Interior",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Toilet",
                table: "Rooms");

            migrationBuilder.AlterColumn<decimal>(
                name: "Area",
                table: "Rooms",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3ae296f8-1fa2-4cbf-9a67-a0bf3f9060c4", "9cefdcb8-5951-4934-a14e-0b4e7c432790", "landlord", "LANDLORD" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "780467e7-e088-4922-ba7e-f4e114ae98e0", "c5731768-da02-4610-9b2f-d0e05c2e4016", "tenant", "TENANT" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d865fd8c-e8ef-4cfe-9f75-8ae3ba316917", "b7319828-14f5-4571-b27a-befcef68dfe7", "admin", "ADMIN" });
        }
    }
}
