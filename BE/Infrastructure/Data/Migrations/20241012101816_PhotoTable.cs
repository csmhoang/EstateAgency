using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class PhotoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Invoices__LeaseI__7D439ABD",
                table: "Invoices");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3ba4d2b0-f869-4bab-a0b7-30e8fc00d898");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "627a74e7-269b-42b5-a5e8-1a0bad66e36f");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8fbab6a5-3377-43ea-8fee-da7921d5305d");

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoomId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "48439f76-b7ec-40f9-b22d-d8df01ee7617", "5dec88a4-dff3-4242-bd29-0dda1135ca32", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "92b11e1b-8bdc-4ba6-957d-07b82a33080b", "6d6cb3af-a197-4754-8bfa-4d8dc808486c", "landlord", "LANDLORD" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d361308c-716e-400a-b011-89de0803ae66", "22ed8e94-0d4c-4fa2-98c9-3f40fef56213", "tenant", "TENANT" });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_RoomId",
                table: "Photos",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK__Invoices__LeaseI__7D439ABD",
                table: "Invoices",
                column: "LeaseId",
                principalTable: "Leases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Invoices__LeaseI__7D439ABD",
                table: "Invoices");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "48439f76-b7ec-40f9-b22d-d8df01ee7617");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "92b11e1b-8bdc-4ba6-957d-07b82a33080b");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d361308c-716e-400a-b011-89de0803ae66");

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Users");

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
                values: new object[] { "3ba4d2b0-f869-4bab-a0b7-30e8fc00d898", "4730e77a-1ebf-41e8-9405-ce0e1a6c174c", "Tenant", "TENANT" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "627a74e7-269b-42b5-a5e8-1a0bad66e36f", "83346ff8-6fe7-41de-9e12-950cb593f137", "Landlord", "LANDLORD" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8fbab6a5-3377-43ea-8fee-da7921d5305d", "454bc8ca-c0cf-4a11-a5a9-773fd5fb8d1c", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK__Invoices__LeaseI__7D439ABD",
                table: "Invoices",
                column: "LeaseId",
                principalTable: "Leases",
                principalColumn: "Id");
        }
    }
}
