using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalManagementSystem.Identity.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDefaultUserDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                column: "NormalizedUserName",
                value: "ADMIN");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "Email", "LastName", "NormalizedEmail", "NormalizedUserName" },
                values: new object[] { "staff1@localhost.com", "Staff1", "STAFF1@LOCALHOST.COM", "STAFF1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                column: "NormalizedUserName",
                value: "ADMIN@LOCALHOST.COM");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "Email", "LastName", "NormalizedEmail", "NormalizedUserName" },
                values: new object[] { "employee@localhost.com", "Staff", "STAFF@LOCALHOST.COM", "STAFF@LOCALHOST.COM" });
        }
    }
}
