using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnicalAdvisor.Migrations
{
    public partial class moreupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dealer_Company_CompanyId",
                table: "Dealer");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Dealer",
                newName: "CompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_Dealer_CompanyId",
                table: "Dealer",
                newName: "IX_Dealer_CompanyID");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyID",
                table: "Dealer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Dealer_Company_CompanyID",
                table: "Dealer",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dealer_Company_CompanyID",
                table: "Dealer");

            migrationBuilder.RenameColumn(
                name: "CompanyID",
                table: "Dealer",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Dealer_CompanyID",
                table: "Dealer",
                newName: "IX_Dealer_CompanyId");

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
