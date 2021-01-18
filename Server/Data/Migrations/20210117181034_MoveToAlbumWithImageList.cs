using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageRepository.Server.Data.Migrations
{
    public partial class MoveToAlbumWithImageList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageCount",
                table: "Albums");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Albums_AlbumID",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_AlbumID",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "ImageCount",
                table: "Albums",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
