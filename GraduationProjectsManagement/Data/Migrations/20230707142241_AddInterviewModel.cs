using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectsManagement.Data.Migrations
{
    public partial class AddInterviewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentGroupId = table.Column<int>(type: "int", nullable: false),
                    SupervisorId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviews", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Admin12345",
                column: "ConcurrencyStamp",
                value: "03a04bd7-d16e-4bd4-b269-82a34e8c82a9");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Student12345",
                column: "ConcurrencyStamp",
                value: "1cced3af-09cd-4af8-a870-a267e82b4351");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Supervisor12345",
                column: "ConcurrencyStamp",
                value: "1a6a3609-3a2c-4625-9d9c-ab2cee1445c3");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "rahafabdo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9637483a-2b45-46ca-ba21-5cfe62fb5e5d", "AQAAAAEAACcQAAAAEN+Xn+h+m4lNz+/BmZ0XM2DhDFrdHDXI6tUSi5rdLUSmKSHvJoaRB1Pxs3uhj8rkfQ==", "daf06f68-34ea-4403-88bf-14db2b673bea" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interviews");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Admin12345",
                column: "ConcurrencyStamp",
                value: "7f1fb158-e760-48b1-9b7b-bdee49888638");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Student12345",
                column: "ConcurrencyStamp",
                value: "e2740f61-37db-4400-a299-b47e683284ba");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Supervisor12345",
                column: "ConcurrencyStamp",
                value: "5f0f887b-4de9-4486-84f5-538760cd5ca1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "rahafabdo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e56aefd8-9cd1-49b8-b684-c1403da777ce", "AQAAAAEAACcQAAAAEOWVchOo2h4+5niClKDMDU7rvuU+rz/UrcUlxls/ghprQqnxgPkJvidCv0FZ99qNrA==", "2666a498-78f3-4269-a3b7-d9df4de3ca51" });
        }
    }
}
