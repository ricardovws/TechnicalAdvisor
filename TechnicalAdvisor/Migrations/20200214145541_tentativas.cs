using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnicalAdvisor.Migrations
{
    public partial class tentativas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "XMLInfoId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_XMLInfoId",
                table: "Product",
                column: "XMLInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_XmlProduct_XMLInfoId",
                table: "Product",
                column: "XMLInfoId",
                principalTable: "XmlProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_XmlProduct_XMLInfoId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_XMLInfoId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "XMLInfoId",
                table: "Product");
        }
    }
}
