using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectsManagement.Data.Migrations
{
    public partial class AddBlogModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Admin12345",
                column: "ConcurrencyStamp",
                value: "982e16bc-bab2-4a55-99eb-f717b63cc119");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Student12345",
                column: "ConcurrencyStamp",
                value: "dfa6d02e-d198-4c48-9e0c-44ab33af9e8c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Supervisor12345",
                column: "ConcurrencyStamp",
                value: "9e136fab-b294-478c-b33a-bd49adfe2c49");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "rahafabdo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e136300e-0263-43f9-896b-45530a994389", "AQAAAAEAACcQAAAAECn+P7wxO6aDnjLJCDXV1H8CBHAPNnfHPiA2gQiPwwZovnf956N3D1Def4l7X1Kjjw==", "8790641c-0005-4aa0-bb2f-5219eddc7a0d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");

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
    }
}
