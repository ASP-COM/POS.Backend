using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.DB.Migrations
{
    /// <inheritdoc />
    public partial class XXXXXX : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_User_ProvidingEmployeeId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "ProvidingEmployeeId",
                table: "Reservations",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ProvidingEmployeeId",
                table: "Reservations",
                newName: "IX_Reservations_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_User_EmployeeId",
                table: "Reservations",
                column: "EmployeeId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_User_EmployeeId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Reservations",
                newName: "ProvidingEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_EmployeeId",
                table: "Reservations",
                newName: "IX_Reservations_ProvidingEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_User_ProvidingEmployeeId",
                table: "Reservations",
                column: "ProvidingEmployeeId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
