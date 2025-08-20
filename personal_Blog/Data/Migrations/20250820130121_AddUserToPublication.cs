using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace personal_Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToPublication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Publication",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publication_UserId",
                table: "Publication",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publication_AspNetUsers_UserId",
                table: "Publication",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publication_AspNetUsers_UserId",
                table: "Publication");

            migrationBuilder.DropIndex(
                name: "IX_Publication_UserId",
                table: "Publication");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Publication");
        }
    }
}
