using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaret.Web.Migrations
{
    public partial class addmigrationmg2313contextWebIdentityContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "newpassword",
                schema: "customer",
                table: "AspNetUsers",
                newName: "Address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                schema: "customer",
                table: "AspNetUsers",
                newName: "newpassword");
        }
    }
}
