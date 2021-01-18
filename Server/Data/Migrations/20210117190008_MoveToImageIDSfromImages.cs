using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageRepository.Server.Data.Migrations
{
    public partial class MoveToImageIDSfromImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Albums_AlbumID",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_AlbumID",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "AlbumID",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "ImagesIDs",
                table: "Albums",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagesIDs",
                table: "Albums");

            migrationBuilder.AddColumn<string>(
                name: "AlbumID",
                table: "Images",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_AlbumID",
                table: "Images",
                column: "AlbumID");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Albums_AlbumID",
                table: "Images",
                column: "AlbumID",
                principalTable: "Albums",
                principalColumn: "AlbumID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
