using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnicalAdvisor.Migrations
{
    public partial class continuandonasimplementacoesdoXML : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AlgumTextoBemAleatorio",
                table: "XmlProduct",
                newName: "TituloDoBloco");

            migrationBuilder.AddColumn<string>(
                name: "InfosDiversas",
                table: "XmlProduct",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkDaImagem",
                table: "XmlProduct",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaisInfos",
                table: "XmlProduct",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InfosDiversas",
                table: "XmlProduct");

            migrationBuilder.DropColumn(
                name: "LinkDaImagem",
                table: "XmlProduct");

            migrationBuilder.DropColumn(
                name: "MaisInfos",
                table: "XmlProduct");

            migrationBuilder.RenameColumn(
                name: "TituloDoBloco",
                table: "XmlProduct",
                newName: "AlgumTextoBemAleatorio");
        }
    }
}
