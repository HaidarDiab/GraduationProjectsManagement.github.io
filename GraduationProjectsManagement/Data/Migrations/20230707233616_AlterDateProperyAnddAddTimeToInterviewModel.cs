using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectsManagement.Data.Migrations
{
    public partial class AlterDateProperyAnddAddTimeToInterviewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InterviewTime",
                table: "Interviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Admin12345",
                column: "ConcurrencyStamp",
                value: "847581df-b6d8-4d8c-aaa7-a918b3bc6c84");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Student12345",
                column: "ConcurrencyStamp",
                value: "9dec1197-6c4b-4318-b3ba-86e8c23d70e0");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Supervisor12345",
                column: "ConcurrencyStamp",
                value: "29a0fd82-7bd5-4f5c-a6fd-e5c29a244117");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "rahafabdo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a4fcf0b8-3967-46d4-8856-5b2dc746d3f0", "AQAAAAEAACcQAAAAEGJ2gn4N6xe5gkUm6I8K3hJtlKwn7I8mXyqYkDNE3pRqKNSLLrOn8hTCHJkGQuzhrQ==", "110aa3c3-ee02-412f-a9b5-0bfb3fdbd10f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterviewTime",
                table: "Interviews");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Admin12345",
                column: "ConcurrencyStamp",
                value: "75798183-f369-48b9-9c3f-0c78e02ba78c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Student12345",
                column: "ConcurrencyStamp",
                value: "5250a973-a256-413c-83d8-374fe23ee83a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Supervisor12345",
                column: "ConcurrencyStamp",
                value: "44f795eb-3580-4e67-ae44-d4155c19cf73");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "rahafabdo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91c15835-275a-4605-b797-c5cccc2ce1c8", "AQAAAAEAACcQAAAAEEtnTlhZLyidFM7xMNnQNIAPkW9SCe8y+pQ1h1WJs433gpwStutxTw0aNsLoa6mXow==", "6282ea41-16fd-4d38-af2b-da2af31c0669" });
        }
    }
}
