using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageRepository.Server.Data.Migrations
{
    public partial class AddImagesDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_AspNetUsers_ApplicationUserId",
                table: "Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_ApplicationUserId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Image");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "Images");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "ImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Image",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_ApplicationUserId",
                table: "Image",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_AspNetUsers_ApplicationUserId",
                table: "Image",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
