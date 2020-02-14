using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnicalAdvisor.Migrations
{
    public partial class tentandoresolverproblemascomXML : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_XmlProduct_Product_ProductId",
                table: "XmlProduct");

            migrationBuilder.DropIndex(
                name: "IX_XmlProduct_ProductId",
                table: "XmlProduct");

            migrationBuilder.AddColumn<int>(
                name: "XmlProductId",
                table: "Product",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_XmlProduct_XmlProductId1",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_XmlProductId1",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "XmlProductId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "XmlProductId1",
                table: "Product");

            migrationBuilder.CreateIndex(
                name: "IX_XmlProduct_ProductId",
                table: "XmlProduct",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_XmlProduct_Product_ProductId",
                table: "XmlProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
