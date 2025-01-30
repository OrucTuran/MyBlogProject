using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlogNight.DataAccessLayer.Migrations
{
    public partial class appUser_ColumnAdded_isAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isAuthor",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAuthor",
                table: "AspNetUsers");
        }
    }
}
