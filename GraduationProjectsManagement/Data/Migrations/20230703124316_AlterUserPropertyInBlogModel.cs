using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectsManagement.Data.Migrations
{
    public partial class AlterUserPropertyInBlogModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Admin12345",
                column: "ConcurrencyStamp",
                value: "eb885142-8613-470a-bc2f-ee416924e8ec");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Student12345",
                column: "ConcurrencyStamp",
                value: "0a6e864a-a666-4d8b-8ff4-058d6dcef93c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "Supervisor12345",
                column: "ConcurrencyStamp",
                value: "768235ac-3da5-4777-94c8-e34d3f2fb559");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "rahafabdo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b39e60a0-ae73-4e87-8851-016b2eaa41f5", "AQAAAAEAACcQAAAAELjQL+T5RpeQN8oqSLjDfutsRgYpb/lWww539LKuTVcB7ZptYrDfsfVkBj/tvjOCwQ==", "49a8134a-d540-4f6a-94fb-d4516943f2e5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
