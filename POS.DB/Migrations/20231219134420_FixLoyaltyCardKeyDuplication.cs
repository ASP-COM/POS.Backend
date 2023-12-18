using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.DB.Migrations
{
    /// <inheritdoc />
    public partial class FixLoyaltyCardKeyDuplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountLoyaltyProgram_LoyaltyProgram_ForLoyaltyProgramsId",
                table: "DiscountLoyaltyProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoyaltyCard_LoyaltyProgram_LoyaltyProgramId",
                table: "LoyaltyCard");

            migrationBuilder.DropForeignKey(
                name: "FK_LoyaltyCard_User_UserId",
                table: "LoyaltyCard");

            migrationBuilder.DropForeignKey(
                name: "FK_LoyaltyProgram_Businesss_BusinessId",
                table: "LoyaltyProgram");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoyaltyProgram",
                table: "LoyaltyProgram");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoyaltyCard",
                table: "LoyaltyCard");

            migrationBuilder.DropColumn(
                name: "LoyaltyId",
                table: "LoyaltyCard");

            migrationBuilder.RenameTable(
                name: "LoyaltyProgram",
                newName: "LoyaltyPrograms");

            migrationBuilder.RenameTable(
                name: "LoyaltyCard",
                newName: "LoyaltyCards");

            migrationBuilder.RenameIndex(
                name: "IX_LoyaltyProgram_BusinessId",
                table: "LoyaltyPrograms",
                newName: "IX_LoyaltyPrograms_BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_LoyaltyCard_UserId",
                table: "LoyaltyCards",
                newName: "IX_LoyaltyCards_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LoyaltyCard_LoyaltyProgramId",
                table: "LoyaltyCards",
                newName: "IX_LoyaltyCards_LoyaltyProgramId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoyaltyPrograms",
                table: "LoyaltyPrograms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoyaltyCards",
                table: "LoyaltyCards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountLoyaltyProgram_LoyaltyPrograms_ForLoyaltyProgramsId",
                table: "DiscountLoyaltyProgram",
                column: "ForLoyaltyProgramsId",
                principalTable: "LoyaltyPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoyaltyCards_LoyaltyPrograms_LoyaltyProgramId",
                table: "LoyaltyCards",
                column: "LoyaltyProgramId",
                principalTable: "LoyaltyPrograms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LoyaltyCards_User_UserId",
                table: "LoyaltyCards",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoyaltyPrograms_Businesss_BusinessId",
                table: "LoyaltyPrograms",
                column: "BusinessId",
                principalTable: "Businesss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountLoyaltyProgram_LoyaltyPrograms_ForLoyaltyProgramsId",
                table: "DiscountLoyaltyProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoyaltyCards_LoyaltyPrograms_LoyaltyProgramId",
                table: "LoyaltyCards");

            migrationBuilder.DropForeignKey(
                name: "FK_LoyaltyCards_User_UserId",
                table: "LoyaltyCards");

            migrationBuilder.DropForeignKey(
                name: "FK_LoyaltyPrograms_Businesss_BusinessId",
                table: "LoyaltyPrograms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoyaltyPrograms",
                table: "LoyaltyPrograms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoyaltyCards",
                table: "LoyaltyCards");

            migrationBuilder.RenameTable(
                name: "LoyaltyPrograms",
                newName: "LoyaltyProgram");

            migrationBuilder.RenameTable(
                name: "LoyaltyCards",
                newName: "LoyaltyCard");

            migrationBuilder.RenameIndex(
                name: "IX_LoyaltyPrograms_BusinessId",
                table: "LoyaltyProgram",
                newName: "IX_LoyaltyProgram_BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_LoyaltyCards_UserId",
                table: "LoyaltyCard",
                newName: "IX_LoyaltyCard_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LoyaltyCards_LoyaltyProgramId",
                table: "LoyaltyCard",
                newName: "IX_LoyaltyCard_LoyaltyProgramId");

            migrationBuilder.AddColumn<int>(
                name: "LoyaltyId",
                table: "LoyaltyCard",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoyaltyProgram",
                table: "LoyaltyProgram",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoyaltyCard",
                table: "LoyaltyCard",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountLoyaltyProgram_LoyaltyProgram_ForLoyaltyProgramsId",
                table: "DiscountLoyaltyProgram",
                column: "ForLoyaltyProgramsId",
                principalTable: "LoyaltyProgram",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoyaltyCard_LoyaltyProgram_LoyaltyProgramId",
                table: "LoyaltyCard",
                column: "LoyaltyProgramId",
                principalTable: "LoyaltyProgram",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LoyaltyCard_User_UserId",
                table: "LoyaltyCard",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoyaltyProgram_Businesss_BusinessId",
                table: "LoyaltyProgram",
                column: "BusinessId",
                principalTable: "Businesss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
