using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class UpdateFeedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReplyId",
                table: "Feedback",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_ReplyId",
                table: "Feedback",
                column: "ReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Feedback_ReplyId",
                table: "Feedback",
                column: "ReplyId",
                principalTable: "Feedback",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Feedback_ReplyId",
                table: "Feedback");

            migrationBuilder.DropIndex(
                name: "IX_Feedback_ReplyId",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "ReplyId",
                table: "Feedback");
        }
    }
}
