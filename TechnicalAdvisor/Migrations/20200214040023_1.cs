using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnicalAdvisor.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_XmlProduct_XmlProductId1",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_XmlProductId1",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "XmlProductId1",
                table: "Product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "XmlProductId1",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_XmlProductId1",
                table: "Product",
                column: "XmlProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_XmlProduct_XmlProductId1",
                table: "Product",
                column: "XmlProductId1",
                principalTable: "XmlProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
