using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectsManagement.Data.Migrations
{
    public partial class ApplyDataAnnotaionOnTheRestOfModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Admin12345",
                column: "ConcurrencyStamp",
                value: "69e9b89a-88a3-4b67-8092-e2421e15d87c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Student12345",
                column: "ConcurrencyStamp",
                value: "78de4240-12ce-47a2-a439-1594782f3e36");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Supervisor12345",
                column: "ConcurrencyStamp",
                value: "1e981954-8218-4104-82ae-8742b760261d");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "rahafabdo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d30b527-9eb8-413d-8f7f-645e67975f07", "AQAAAAEAACcQAAAAEEE4CFrcjxodpw09o22eMCVBkbcjZRZK5G37sUfHIyP3vQhCCYLGTEuC+MA5SMaxkw==", "56b0207e-6a46-44da-99cd-aa03ec6c9dff" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Admin12345",
                column: "ConcurrencyStamp",
                value: "dadcae5f-a3e4-423c-b095-aa1465947bbe");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Student12345",
                column: "ConcurrencyStamp",
                value: "c4c863a8-0a0f-4899-a928-6d20f0e4e024");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Supervisor12345",
                column: "ConcurrencyStamp",
                value: "faac6b4f-1b20-4264-b542-cf3e1eb5aea7");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "rahafabdo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6acbafe0-0c33-4a64-8b55-79f74a0804a1", "AQAAAAEAACcQAAAAEHFr7wjgD9+Hrw+6UOJanIS9l+ODXGL1KVyjAat3kc7cape/I/lTGEZmpgMCTMnEOA==", "cfdd684c-e821-46e5-84d0-14c1827886d5" });
        }
    }
}
