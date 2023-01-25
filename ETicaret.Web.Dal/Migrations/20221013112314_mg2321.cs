using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaret.Shared.Dal.Migrations
{
    public partial class mg2321 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressTitle",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressTitle",
                table: "Addresses");
        }
    }
}
