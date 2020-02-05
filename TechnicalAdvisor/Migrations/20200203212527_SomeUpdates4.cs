using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnicalAdvisor.Migrations
{
    public partial class SomeUpdates4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dealer_Company_CompanyId",
                table: "Dealer");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Dealer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Dealer_Company_CompanyId",
                table: "Dealer",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dealer_Company_CompanyId",
                table: "Dealer");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Dealer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Dealer_Company_CompanyId",
                table: "Dealer",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
