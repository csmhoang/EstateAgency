using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class AddFollowVsFavorite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ__Users__1DF52D0C64B859D5",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "UQ__Rooms__4F9D52313B1CAD3E",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "UQ__Reservat__2081C0BBCBCC7940",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "UQ__Posts__5K9D52454DASDASE",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "UQ__Payments__106D3BA8FEBBB92E",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "UQ__Messages__54E8229FE4E7C357",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "UQ__Maintena__CBAB82F6EC2CDA6A",
                table: "MaintenanceRequests");

            migrationBuilder.DropIndex(
                name: "UQ__Leases__D568E4B4702D9CD2",
                table: "Leases");

            migrationBuilder.DropIndex(
                name: "UQ__Invoices__0D9D7FF34AA36732",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "UQ__Feedback__BCE37B663CEFBD99",
                table: "Feedback");

            migrationBuilder.DropIndex(
                name: "UQ__Amenitie__300F6CA3021E4D38",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "UserCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoomCode",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ReservationCode",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PaymentCode",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "MessageCode",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MaintenanceRequestCode",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "LeaseCode",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "InvoiceCode",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "FeedbackCode",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "AmenityCode",
                table: "Amenities");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Rooms",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Province",
                table: "Rooms",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Reservations",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Posts",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "MaintenanceRequests",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Leases",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Amenities",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "lower(newid())"),
                    UserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Follows",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "lower(newid())"),
                    FollowerId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Follows_Users_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SavePosts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "lower(newid())"),
                    FavoriteId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    PostId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavePosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavePosts_Favorites_FavoriteId",
                        column: x => x.FavoriteId,
                        principalTable: "Favorites",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SavePosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FollowerId",
                table: "Follows",
                column: "FollowerId");

            migrationBuilder.CreateIndex(
                name: "IX_SavePosts_FavoriteId",
                table: "SavePosts",
                column: "FavoriteId");

            migrationBuilder.CreateIndex(
                name: "IX_SavePosts_PostId",
                table: "SavePosts",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Follows");

            migrationBuilder.DropTable(
                name: "SavePosts");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<int>(
                name: "UserCode",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Rooms",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Province",
                table: "Rooms",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "RoomCode",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Reservations",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<int>(
                name: "ReservationCode",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Posts",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<int>(
                name: "PostCode",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PaymentCode",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "MessageCode",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "MaintenanceRequests",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<int>(
                name: "MaintenanceRequestCode",
                table: "MaintenanceRequests",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Leases",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<int>(
                name: "LeaseCode",
                table: "Leases",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceCode",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "FeedbackCode",
                table: "Feedback",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Amenities",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<int>(
                name: "AmenityCode",
                table: "Amenities",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__1DF52D0C64B859D5",
                table: "Users",
                column: "UserCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Rooms__4F9D52313B1CAD3E",
                table: "Rooms",
                column: "RoomCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Reservat__2081C0BBCBCC7940",
                table: "Reservations",
                column: "ReservationCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Posts__5K9D52454DASDASE",
                table: "Posts",
                column: "PostCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Payments__106D3BA8FEBBB92E",
                table: "Payments",
                column: "PaymentCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Messages__54E8229FE4E7C357",
                table: "Messages",
                column: "MessageCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Maintena__CBAB82F6EC2CDA6A",
                table: "MaintenanceRequests",
                column: "MaintenanceRequestCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Leases__D568E4B4702D9CD2",
                table: "Leases",
                column: "LeaseCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Invoices__0D9D7FF34AA36732",
                table: "Invoices",
                column: "InvoiceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Feedback__BCE37B663CEFBD99",
                table: "Feedback",
                column: "FeedbackCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Amenitie__300F6CA3021E4D38",
                table: "Amenities",
                column: "AmenityCode",
                unique: true);
        }
    }
}
