using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaret.Web.Migrations
{
    public partial class mg2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "newpassword",
                schema: "customer",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "newpassword",
                schema: "customer",
                table: "AspNetUsers");
        }
    }
}
