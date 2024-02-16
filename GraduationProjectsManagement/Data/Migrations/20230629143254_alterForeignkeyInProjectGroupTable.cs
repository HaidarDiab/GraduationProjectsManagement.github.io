using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectsManagement.Data.Migrations
{
    public partial class alterForeignkeyInProjectGroupTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Admin12345",
                column: "ConcurrencyStamp",
                value: "9d838722-e141-492f-b20d-de873e74cede");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Student12345",
                column: "ConcurrencyStamp",
                value: "6e8a74fe-b455-4c60-8800-38b2ce7849eb");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Supervisor12345",
                column: "ConcurrencyStamp",
                value: "a329e8d6-4338-4ea6-8dcb-f6b9c5980d6c");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "rahafabdo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7a607ca8-3789-40f8-b962-434bfa33db91", "AQAAAAEAACcQAAAAECrd2OTxw5A9QGDSfSnHGkylLLd13Kw2T7Qx717DnaEWR73VawvC2GA83SZDEYAQRg==", "a4994b25-c0e3-46f8-bd3f-b4bc81840bb9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
