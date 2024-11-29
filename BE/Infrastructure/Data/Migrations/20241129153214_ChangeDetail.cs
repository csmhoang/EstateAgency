using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class ChangeDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Carts_TenantId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "IntendedIntoDate",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "IntendedIntoDate",
                table: "BookingDetails");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfMonth",
                table: "CartDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfMonth",
                table: "BookingDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_TenantId",
                table: "Carts",
                column: "TenantId",
                unique: true,
                filter: "[TenantId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Carts_TenantId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "NumberOfMonth",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "NumberOfMonth",
                table: "BookingDetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "CartDetails",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "IntendedIntoDate",
                table: "CartDetails",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "BookingDetails",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "IntendedIntoDate",
                table: "BookingDetails",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Carts_TenantId",
                table: "Carts",
                column: "TenantId");
        }
    }
}
