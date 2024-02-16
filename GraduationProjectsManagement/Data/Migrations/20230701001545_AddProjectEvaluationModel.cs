using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectsManagement.Data.Migrations
{
    public partial class AddProjectEvaluationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectEvaluations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    GradeLevelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEvaluations", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Admin12345",
                column: "ConcurrencyStamp",
                value: "ac890b60-95a0-4f7e-a9fd-22a73afe0cd4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Student12345",
                column: "ConcurrencyStamp",
                value: "6b42c4da-606a-466e-abe3-57162a7a5274");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Supervisor12345",
                column: "ConcurrencyStamp",
                value: "4312d4e4-4b73-46b8-8941-d3b43b16eadb");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "rahafabdo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "da97a0a7-1616-4dff-b031-84206547cce5", "AQAAAAEAACcQAAAAECsP9yc+9JNfK4IremB1aTRvooNhmC1N5vY3fsnTDRXuqmH2qxn7oKtj4LZKg/gsVA==", "d3db9065-2d89-44c7-b1cd-d6db8791f2fe" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectEvaluations");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Admin12345",
                column: "ConcurrencyStamp",
                value: "d5642a38-ef4b-4fed-81ee-c9f9c5a10cc3");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Student12345",
                column: "ConcurrencyStamp",
                value: "5b22867f-5d9b-43b0-9968-107ce529227c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Supervisor12345",
                column: "ConcurrencyStamp",
                value: "18938cf6-e9f1-4892-a2f8-aca1abe7a9d8");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "rahafabdo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aee54dcd-7f8e-4990-9a23-88a561f590b1", "AQAAAAEAACcQAAAAEEgJoRjHAF3+MilSdyfuBDdeNSGc/cOCjIrLd3y+HbuWqVn0ItaYtKPn1DHodjX0OA==", "916079e6-c492-4a54-83df-f063dba1044f" });
        }
    }
}
