using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class UpdateLease : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaseDetails_Bookings_BookingId",
                table: "LeaseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaseDetails_Rooms_RoomId",
                table: "LeaseDetails");

            migrationBuilder.DropIndex(
                name: "IX_LeaseDetails_BookingId",
                table: "LeaseDetails");

            migrationBuilder.DropColumn(
                name: "IsConfirm",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "LeaseDetails");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "LeaseDetails");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "LeaseDetails");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "LeaseDetails",
                newName: "Price");

            migrationBuilder.AlterColumn<string>(
                name: "Terms",
                table: "Leases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SignedDate",
                table: "Leases",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "BookingId",
                table: "Leases",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lessee",
                table: "Leases",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lessor",
                table: "Leases",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "RoomId",
                table: "LeaseDetails",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "LeaseDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfMonth",
                table: "LeaseDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfTenant",
                table: "LeaseDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Leases_BookingId",
                table: "Leases",
                column: "BookingId",
                unique: true,
                filter: "[BookingId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaseDetails_Rooms_RoomId",
                table: "LeaseDetails",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_Bookings_BookingId",
                table: "Leases",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaseDetails_Rooms_RoomId",
                table: "LeaseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Leases_Bookings_BookingId",
                table: "Leases");

            migrationBuilder.DropIndex(
                name: "IX_Leases_BookingId",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "Lessee",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "Lessor",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "NumberOfMonth",
                table: "LeaseDetails");

            migrationBuilder.DropColumn(
                name: "NumberOfTenant",
                table: "LeaseDetails");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "LeaseDetails",
                newName: "Amount");

            migrationBuilder.AlterColumn<string>(
                name: "Terms",
                table: "Leases",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SignedDate",
                table: "Leases",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirm",
                table: "Leases",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "RoomId",
                table: "LeaseDetails",
                type: "nvarchar(36)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "LeaseDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookingId",
                table: "LeaseDetails",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "LeaseDetails",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "LeaseDetails",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeaseDetails_BookingId",
                table: "LeaseDetails",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaseDetails_Bookings_BookingId",
                table: "LeaseDetails",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaseDetails_Rooms_RoomId",
                table: "LeaseDetails",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }
    }
}
