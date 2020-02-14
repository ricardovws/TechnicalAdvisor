using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnicalAdvisor.Migrations
{
    public partial class somealgunstestesdasimplmentacoesXML : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "XmlProduct",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "XmlProduct",
                newName: "FileName");

            migrationBuilder.AddColumn<string>(
                name: "AlgumTextoBemAleatorio",
                table: "XmlProduct",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "XmlProduct",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_XmlProduct_Product_ProductId",
                table: "XmlProduct");

            migrationBuilder.DropIndex(
                name: "IX_XmlProduct_ProductId",
                table: "XmlProduct");

            migrationBuilder.DropColumn(
                name: "AlgumTextoBemAleatorio",
                table: "XmlProduct");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "XmlProduct");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "XmlProduct",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "XmlProduct",
                newName: "Name");
        }
    }
}
