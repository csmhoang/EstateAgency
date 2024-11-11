using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class AddUserRoleNewSecond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_AspNetUsers_UserId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_AspNetUsers_TenantId",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_AspNetUsers_FollowerId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Leases_AspNetUsers_TenantId",
                table: "Leases");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_TenantId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_AspNetUsers_LandlordId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "UserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "Roles");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Users_UserId",
                table: "Favorites",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Users_TenantId",
                table: "Feedback",
                column: "TenantId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_Users_FollowerId",
                table: "Follows",
                column: "FollowerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_Users_TenantId",
                table: "Leases",
                column: "TenantId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_TenantId",
                table: "Reservations",
                column: "TenantId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_LandlordId",
                table: "Rooms",
                column: "LandlordId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Users_UserId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Users_TenantId",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_Users_FollowerId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Leases_Users_TenantId",
                table: "Leases");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_TenantId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_LandlordId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "AspNetRoles");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_AspNetUsers_UserId",
                table: "Favorites",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_AspNetUsers_TenantId",
                table: "Feedback",
                column: "TenantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_AspNetUsers_FollowerId",
                table: "Follows",
                column: "FollowerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_AspNetUsers_TenantId",
                table: "Leases",
                column: "TenantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_TenantId",
                table: "Reservations",
                column: "TenantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_AspNetUsers_LandlordId",
                table: "Rooms",
                column: "LandlordId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
