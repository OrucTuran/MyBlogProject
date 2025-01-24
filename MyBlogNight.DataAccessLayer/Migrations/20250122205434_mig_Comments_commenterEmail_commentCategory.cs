using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlogNight.DataAccessLayer.Migrations
{
    public partial class mig_Comments_commenterEmail_commentCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CommenterEmail",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CategoryId",
                table: "Comments",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Categories_CategoryId",
                table: "Comments",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Categories_CategoryId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CategoryId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CommenterEmail",
                table: "Comments");
        }
    }
}
