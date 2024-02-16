using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectsManagement.Data.Migrations
{
    public partial class ExtendSentMailAndReceiveMailwithDateTimeProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SentDate",
                table: "SentMails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceiveDate",
                table: "ReceivedMails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentDate",
                table: "SentMails");

            migrationBuilder.DropColumn(
                name: "ReceiveDate",
                table: "ReceivedMails");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Admin12345",
                column: "ConcurrencyStamp",
                value: "571c90be-c3c6-4f3e-9710-ead9fe2118c7");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Student12345",
                column: "ConcurrencyStamp",
                value: "865c3937-5224-4aa5-825b-312e1b70aa57");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Supervisor12345",
                column: "ConcurrencyStamp",
                value: "1e2c8998-a89f-4a45-8efc-3d3dc302e376");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "rahafabdo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "322ad8cb-1572-4dd6-9756-0cc8badca104", "AQAAAAEAACcQAAAAEM8pNPn+AaQdHdWaddBzsbn1/CKBISt5HWnv3uYqNnyj8mSKsVD4T4J5kGAb07tr2Q==", "31c4e83f-19c2-4155-85e8-84a4f14acf55" });
        }
    }
}
