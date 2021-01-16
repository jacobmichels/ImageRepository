using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageRepository.Server.Data.Migrations
{
    public partial class AddContentTypeToImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Images",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Images");
        }
    }
}
