using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourCompany.Infrastructure.Migrations
{
    public partial class Guide_Active_Behaviour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Guides",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Guides");
        }
    }
}
