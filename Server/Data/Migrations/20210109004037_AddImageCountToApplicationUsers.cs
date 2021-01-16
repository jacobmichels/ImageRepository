using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageRepository.Server.Data.Migrations
{
    public partial class AddImageCountToApplicationUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageCount",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageCount",
                table: "AspNetUsers");
        }
    }
}
