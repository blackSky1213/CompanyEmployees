using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyEmployees.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4a947012-bf0f-49d0-97cc-bba77d4cdea2", "f5bc0d1d-676c-4567-883a-6013c57b79ff", "Manager", "MANAGER" },
                    { "c847583d-d8b2-42de-a1d8-7e3ca53c320b", "e2bc477b-ecbf-4eaf-bda9-6695740ce915", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a947012-bf0f-49d0-97cc-bba77d4cdea2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c847583d-d8b2-42de-a1d8-7e3ca53c320b");
        }
    }
}
