using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Carl_Assignment.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE SEQUENCE ProductID_Sequence START WITH 100002 INCREMENT BY 1;");
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR ProductID_Sequence"),
                    ProductName = table.Column<string>(maxLength: 50, nullable: false),
                    Category = table.Column<string>(nullable: true),
                    Stock = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Createdon = table.Column<DateTime>(nullable: true, defaultValue: new DateTime(2024, 8, 27, 1, 18, 46, 745, DateTimeKind.Local).AddTicks(5653))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Category", "Description", "ProductName", "Stock" },
                values: new object[] { 100000, "Furniture", "New Furnitures", "Chair", 100 });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Category", "Description", "ProductName", "Stock" },
                values: new object[] { 100001, "Clothes", "New Clothes", "Shirt", 100 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP SEQUENCE ProductID_Sequence");
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
