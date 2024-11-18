using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class DeleteFavorite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavePosts_Favorites_FavoriteId",
                table: "SavePosts");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.RenameColumn(
                name: "FavoriteId",
                table: "SavePosts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SavePosts_FavoriteId",
                table: "SavePosts",
                newName: "IX_SavePosts_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavePosts_Users_UserId",
                table: "SavePosts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavePosts_Users_UserId",
                table: "SavePosts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SavePosts",
                newName: "FavoriteId");

            migrationBuilder.RenameIndex(
                name: "IX_SavePosts_UserId",
                table: "SavePosts",
                newName: "IX_SavePosts_FavoriteId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_SavePosts_Favorites_FavoriteId",
                table: "SavePosts",
                column: "FavoriteId",
                principalTable: "Favorites",
                principalColumn: "Id");
        }
    }
}
