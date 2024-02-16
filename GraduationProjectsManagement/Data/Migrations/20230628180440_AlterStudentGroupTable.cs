using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectsManagement.Data.Migrations
{
    public partial class AlterStudentGroupTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentGroups_Users_StudentId",
                table: "StudentGroups");

            migrationBuilder.DropIndex(
                name: "IX_StudentGroups_StudentId",
                table: "StudentGroups");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "StudentGroups",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "StudentGroups",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Admin12345",
                column: "ConcurrencyStamp",
                value: "fe391e20-b512-4606-aca9-5b8e7ce1d354");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Student12345",
                column: "ConcurrencyStamp",
                value: "6dc93bb1-126d-4017-b112-3184f5449ed9");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Supervisor12345",
                column: "ConcurrencyStamp",
                value: "db4515b9-c0ae-4df9-a0dc-7f71b970ab42");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "rahafabdo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8c3b2ffe-5714-41e8-92e0-2ea1730eb25c", "AQAAAAEAACcQAAAAEJKyHpBVNKCmIyG4ExaZdm0jItgQhPZ/RxypRgoVlvgVVQoqn4FQkqqUEJVgYqpvXw==", "0f8c9f2b-967d-4895-8d08-69bb0f22a3da" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroups_StudentId",
                table: "StudentGroups",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGroups_Users_StudentId",
                table: "StudentGroups",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
