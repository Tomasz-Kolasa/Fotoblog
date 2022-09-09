using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fotoblog.DAL.Migrations
{
    public partial class PhotoEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "PhotoEntities",
                newName: "ThumbnailUrl");

            migrationBuilder.RenameColumn(
                name: "ImageExtension",
                table: "PhotoEntities",
                newName: "Path");

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "PhotoEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OriginalUrl",
                table: "PhotoEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "PhotoEntities");

            migrationBuilder.DropColumn(
                name: "OriginalUrl",
                table: "PhotoEntities");

            migrationBuilder.RenameColumn(
                name: "ThumbnailUrl",
                table: "PhotoEntities",
                newName: "ImagePath");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "PhotoEntities",
                newName: "ImageExtension");
        }
    }
}
