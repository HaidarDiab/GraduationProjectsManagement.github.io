using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectsManagement.Data.Migrations
{
    public partial class AddIsSelectedPropertyToProjectsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Admin12345",
                column: "ConcurrencyStamp",
                value: "9be640d2-f802-433d-9974-a6c6a014ab30");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Student12345",
                column: "ConcurrencyStamp",
                value: "7f91ebd3-ea38-464a-b557-02caef38a905");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Supervisor12345",
                column: "ConcurrencyStamp",
                value: "21cefc13-58e1-4211-8c23-db06a1c17c2b");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "rahafabdo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e380613a-075e-4f8b-8b14-46b3903947de", "AQAAAAEAACcQAAAAEKfwN3ncqP9VASdNZu9+c9CtLIwH4vIXvnZ35fnZC29oFeoBt3KohdbnhIbh2iTawQ==", "417b2c7a-931f-4d63-a28b-200192bafb0e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "Projects");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Admin12345",
                column: "ConcurrencyStamp",
                value: "8a9ec7a8-f88c-42d1-abf0-2b9eb757bd2e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Student12345",
                column: "ConcurrencyStamp",
                value: "249265bb-7b93-4af6-a621-05558d881cd9");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Supervisor12345",
                column: "ConcurrencyStamp",
                value: "6486ecf2-c486-43e0-8925-e97a395c1507");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "rahafabdo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a73975b9-736b-40cd-8ee1-d104a9272fb7", "AQAAAAEAACcQAAAAEDE5swG4DoNevM78rklCOU98L28bPpyg/fOtOk6UkxW81Jiynhe346NE1AdEEd2eTA==", "290ba267-4941-4455-87cc-48fff3083498" });
        }
    }
}
