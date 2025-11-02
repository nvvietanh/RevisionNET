using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Revision.LINQ.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, "Điện tử", "Laptop Dell XPS 13", 25000000m, 15 },
                    { 2, "Điện tử", "iPhone 15 Pro Max", 30000000m, 8 },
                    { 3, "Điện tử", "Bàn phím cơ Keychron K2", 2500000m, 25 },
                    { 4, "Điện tử", "Chuột Logitech MX Master 3", 2000000m, 30 },
                    { 5, "Sách", "Sách: Clean Code", 250000m, 50 },
                    { 6, "Sách", "Sách: Design Patterns", 300000m, 35 },
                    { 7, "Điện tử", "Tai nghe Sony WH-1000XM5", 8000000m, 12 },
                    { 8, "Điện tử", "Màn hình LG 27 inch 4K", 7500000m, 10 },
                    { 9, "Điện tử", "Webcam Logitech C920", 1800000m, 20 },
                    { 10, "Sách", "Sách: Refactoring", 280000m, 40 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
