using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourCompany.Infrastructure.Migrations
{
    public partial class Gender_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genders",
                type: "nchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(5)",
                oldMaxLength: 5);

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "GenderID", "Name" },
                values: new object[] { 1, "Erkek" });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "GenderID", "Name" },
                values: new object[] { 2, "Kadın" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "GenderID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "GenderID",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genders",
                type: "char(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(5)",
                oldMaxLength: 5);
        }
    }
}
