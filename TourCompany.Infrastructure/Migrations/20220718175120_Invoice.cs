using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourCompany.Infrastructure.Migrations
{
    public partial class Invoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNo = table.Column<string>(type: "char(14)", maxLength: 14, nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    TouristID = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceID);
                    table.UniqueConstraint("AK_Invoices_InvoiceID_TouristID", x => new { x.InvoiceID, x.TouristID });
                    table.ForeignKey(
                        name: "FK_Invoices_Tourists_TouristID",
                        column: x => x.TouristID,
                        principalTable: "Tourists",
                        principalColumn: "TouristID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    InvoiceID = table.Column<int>(type: "int", nullable: false),
                    BookingID = table.Column<int>(type: "int", nullable: false),
                    TouristID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => new { x.InvoiceID, x.BookingID, x.TouristID });
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceID_TouristID",
                        columns: x => new { x.InvoiceID, x.TouristID },
                        principalTable: "Invoices",
                        principalColumns: new[] { "InvoiceID", "TouristID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_TourParticipants_BookingID_TouristID",
                        columns: x => new { x.BookingID, x.TouristID },
                        principalTable: "TourParticipants",
                        principalColumns: new[] { "BookingID", "TouristID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_BookingID_TouristID",
                table: "InvoiceItems",
                columns: new[] { "BookingID", "TouristID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceID_TouristID",
                table: "InvoiceItems",
                columns: new[] { "InvoiceID", "TouristID" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_TouristID",
                table: "Invoices",
                column: "TouristID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "Invoices");
        }
    }
}
