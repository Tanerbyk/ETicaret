using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaret.Shared.Dal.Migrations
{
    public partial class filePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Imageurl",
                table: "Products",
                newName: "Path");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Products",
                newName: "Imageurl");
        }
    }
}
