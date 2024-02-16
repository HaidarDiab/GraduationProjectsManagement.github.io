using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectsManagement.Data.Migrations
{
    public partial class AddUserIdAsForeignKeyToBlogModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Admin12345",
                column: "ConcurrencyStamp",
                value: "1533360e-e655-4661-bd5b-30b11f995d6a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Student12345",
                column: "ConcurrencyStamp",
                value: "1279e829-6c69-4b79-ae81-84260db78620");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Supervisor12345",
                column: "ConcurrencyStamp",
                value: "099007bc-605b-4704-926e-4cc2257e3205");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "rahafabdo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ebf2eac-5f7c-42f3-a90b-ad809b8729f5", "AQAAAAEAACcQAAAAENzVV0nkpmkdN6tnW1wI6wQf1Y29U8KZiJAwCa+PUlRbsk6fmKGp2JlpF8NjHTN6nA==", "c8a8b2e6-d302-49eb-ad88-7ca702b262f1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Blogs");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Admin12345",
                column: "ConcurrencyStamp",
                value: "f603d9f5-4670-472b-a0e9-315e29ff5fa0");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Student12345",
                column: "ConcurrencyStamp",
                value: "839ea679-5a93-4316-bc7f-cdf42ec2ac39");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Supervisor12345",
                column: "ConcurrencyStamp",
                value: "7a74699a-b43e-4b12-b5a2-b7b92b33c604");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "rahafabdo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de929263-8b45-4478-a57a-228a6b248b14", "AQAAAAEAACcQAAAAEH4K6aeR8CcrWrlKb/M/aJBlYTP8OQFSo6j4mdrCXUNEZ5OOQ3BzvcILyjNbDfNBwQ==", "c33cb50f-5b8f-4aea-a51a-7c4d2513f8bc" });
        }
    }
}
