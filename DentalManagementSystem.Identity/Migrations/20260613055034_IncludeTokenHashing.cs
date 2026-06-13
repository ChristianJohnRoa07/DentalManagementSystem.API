using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalManagementSystem.Identity.Migrations
{
    /// <inheritdoc />
    public partial class IncludeTokenHashing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BlacklistedTokens_Token",
                table: "BlacklistedTokens");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "BlacklistedTokens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AddColumn<string>(
                name: "TokenHash",
                table: "BlacklistedTokens",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BlacklistedTokens_TokenHash",
                table: "BlacklistedTokens",
                column: "TokenHash",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BlacklistedTokens_TokenHash",
                table: "BlacklistedTokens");

            migrationBuilder.DropColumn(
                name: "TokenHash",
                table: "BlacklistedTokens");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "BlacklistedTokens",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_BlacklistedTokens_Token",
                table: "BlacklistedTokens",
                column: "Token",
                unique: true);
        }
    }
}
