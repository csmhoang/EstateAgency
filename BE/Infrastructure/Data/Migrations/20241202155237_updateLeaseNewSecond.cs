using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class updateLeaseNewSecond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "LeaseDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "LeaseDetails");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BookingDetails");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Leases",
                type: "nvarchar(265)",
                maxLength: 265,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Leases");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "LeaseDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "LeaseDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "BookingDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "BookingDetails",
                type: "datetime2",
                nullable: true);
        }
    }
}
