using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class addinformationroom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3b4c91bf-b200-4b47-adf7-3bf205938578");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "87d8b645-37bb-4498-936e-8cfec726b8b0");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "be2c26d2-3e59-40ff-a897-2317d3fb1d1e");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AvailableFrom",
                table: "Rooms",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bathroom",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Bedroom",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Rooms",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ward",
                table: "Rooms",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "837f1c9f-a75e-4394-a329-596c793098e2", "c27b4e68-a2a8-45dc-bbad-bb6ce78dd988", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8af6cd3c-2f10-4075-85b1-f3c1c95f6b9d", "a1cc1e75-e58e-4f9b-846b-ac14674e798c", "landlord", "LANDLORD" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c66e11ef-d6e9-4641-927e-9a932b42a0dd", "417c145a-ad41-44e6-ae3e-23770e545565", "tenant", "TENANT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "837f1c9f-a75e-4394-a329-596c793098e2");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8af6cd3c-2f10-4075-85b1-f3c1c95f6b9d");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "c66e11ef-d6e9-4641-927e-9a932b42a0dd");

            migrationBuilder.DropColumn(
                name: "Bathroom",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Bedroom",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Ward",
                table: "Rooms");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AvailableFrom",
                table: "Rooms",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3b4c91bf-b200-4b47-adf7-3bf205938578", "d7a7ff39-e543-4cd2-a74a-0b50c98808d9", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "87d8b645-37bb-4498-936e-8cfec726b8b0", "61d6e82c-88cc-4d8b-9ca9-2b9a3467039d", "tenant", "TENANT" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "be2c26d2-3e59-40ff-a897-2317d3fb1d1e", "43c15c40-744c-4334-affe-ae138a826bb6", "landlord", "LANDLORD" });
        }
    }
}
