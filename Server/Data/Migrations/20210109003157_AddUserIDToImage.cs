using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageRepository.Server.Data.Migrations
{
    public partial class AddUserIDToImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Images",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Images");
        }
    }
}
