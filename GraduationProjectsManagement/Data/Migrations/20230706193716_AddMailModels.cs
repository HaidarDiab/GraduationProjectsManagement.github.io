using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectsManagement.Data.Migrations
{
    public partial class AddMailModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MailStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceivedMails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceivingStatus = table.Column<bool>(type: "bit", nullable: false),
                    MailStatusId = table.Column<int>(type: "int", nullable: false),
                    ReceivedUserUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivedMails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SentMails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendingStatus = table.Column<bool>(type: "bit", nullable: false),
                    MailStatusId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentMails", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MailStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "صادر" },
                    { 2, "وارد" }
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailStatuses");

            migrationBuilder.DropTable(
                name: "ReceivedMails");

            migrationBuilder.DropTable(
                name: "SentMails");

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
    }
}
