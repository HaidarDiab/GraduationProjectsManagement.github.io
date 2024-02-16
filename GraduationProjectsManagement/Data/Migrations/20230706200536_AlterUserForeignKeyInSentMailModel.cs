using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectsManagement.Data.Migrations
{
    public partial class AlterUserForeignKeyInSentMailModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SentMails",
                newName: "SentUserId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SentUserId",
                table: "SentMails",
                newName: "UserId");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Admin12345",
                column: "ConcurrencyStamp",
                value: "c4892e8a-e27a-401b-a2d3-7f9e0c44d572");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Student12345",
                column: "ConcurrencyStamp",
                value: "a6e35d55-7c8d-4fb0-974a-50e24776b861");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Supervisor12345",
                column: "ConcurrencyStamp",
                value: "b937b407-24b3-4173-bcd6-318e119b2abb");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "rahafabdo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11690d6d-ed6e-46eb-a573-10b20e9921f8", "AQAAAAEAACcQAAAAEJ5GKZXmmW0QcE+HDYHHZJjW+l9QU+ATy7LXL42HvezfP90rx2kjgEqt+LxYzFXaBQ==", "5ef5e13b-2f1d-4e87-9f42-151c6f517ccf" });
        }
    }
}
