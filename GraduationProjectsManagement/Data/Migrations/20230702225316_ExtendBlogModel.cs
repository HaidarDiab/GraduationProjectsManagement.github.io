using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectsManagement.Data.Migrations
{
    public partial class ExtendBlogModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Question",
                table: "Blogs");

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
    }
}
