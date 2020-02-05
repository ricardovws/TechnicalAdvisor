using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnicalAdvisor.Migrations
{
    public partial class SomeUpdates2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Dealer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Dealer");
        }
    }
}
