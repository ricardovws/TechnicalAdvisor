using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnicalAdvisor.Migrations
{
    public partial class implemntandocampodeimagemnocadastrodeproduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureName",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureName",
                table: "Product");
        }
    }
}
