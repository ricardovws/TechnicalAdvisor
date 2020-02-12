using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnicalAdvisor.Migrations
{
    public partial class doingprogress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Dealer_DealerId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "DealerId",
                table: "User",
                newName: "DealerID");

            migrationBuilder.RenameIndex(
                name: "IX_User_DealerId",
                table: "User",
                newName: "IX_User_DealerID");

            migrationBuilder.AlterColumn<int>(
                name: "DealerID",
                table: "User",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Dealer_DealerID",
                table: "User",
                column: "DealerID",
                principalTable: "Dealer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Dealer_DealerID",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "DealerID",
                table: "User",
                newName: "DealerId");

            migrationBuilder.RenameIndex(
                name: "IX_User_DealerID",
                table: "User",
                newName: "IX_User_DealerId");

            migrationBuilder.AlterColumn<int>(
                name: "DealerId",
                table: "User",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_User_Dealer_DealerId",
                table: "User",
                column: "DealerId",
                principalTable: "Dealer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
