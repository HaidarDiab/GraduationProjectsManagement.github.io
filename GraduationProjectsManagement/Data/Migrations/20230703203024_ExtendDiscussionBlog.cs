using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectsManagement.Data.Migrations
{
    public partial class ExtendDiscussionBlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AnswerDate",
                table: "DiscussionBlogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Admin12345",
                column: "ConcurrencyStamp",
                value: "f8335846-3804-4fd2-b632-86651c0db81d");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Student12345",
                column: "ConcurrencyStamp",
                value: "6c3ae4d9-90d0-430f-a21b-bac88d80580a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Supervisor12345",
                column: "ConcurrencyStamp",
                value: "58fab0bc-302f-43e5-b9cb-8e06404d8bae");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "rahafabdo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c0d9f18f-48c4-4c18-a954-f9cad0424e71", "AQAAAAEAACcQAAAAEC++relNOvBPZszOvds2tFx76MM+WlNI4bQ4kaBAOKt05GAN7enzXbMB7VzQeY3qsA==", "2d39f1be-52ff-4c9e-b83f-3eb2830b2f51" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerDate",
                table: "DiscussionBlogs");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Admin12345",
                column: "ConcurrencyStamp",
                value: "1d43e4ed-3c37-4021-9663-fcb4ce9fa4cf");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Student12345",
                column: "ConcurrencyStamp",
                value: "8e885180-3fd3-409b-89c9-b5c3ede81db5");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Supervisor12345",
                column: "ConcurrencyStamp",
                value: "aa6668a6-d35b-42cb-a028-a36876ce3e35");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "rahafabdo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "507ff012-d035-441a-b1c7-3362d2c5d45c", "AQAAAAEAACcQAAAAELx74ozSpzo+xvHlEeBfBsxc/QFnbiTHqFl9txGEH8UlUvwk6aSxBdB0W6uuhUt/mw==", "dbdb66f9-22ea-4ed2-870d-6a09b928aa5b" });
        }
    }
}
