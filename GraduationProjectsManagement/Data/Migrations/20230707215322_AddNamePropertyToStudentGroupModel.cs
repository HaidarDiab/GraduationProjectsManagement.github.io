using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectsManagement.Data.Migrations
{
    public partial class AddNamePropertyToStudentGroupModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "StudentGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "StudentGroups");

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
    }
}
