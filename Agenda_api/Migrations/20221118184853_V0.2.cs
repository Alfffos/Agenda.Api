using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agenda_api.Migrations
{
    public partial class V02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Users_UserId",
                table: "Contact");

            migrationBuilder.AddColumn<int>(
                name: "Create_And_Update_User_DTOId",
                table: "Contact",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contatc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CelularNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    TelephoneNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contatc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contatc_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Create_And_Update_User_DTOId",
                table: "Contact",
                column: "Create_And_Update_User_DTOId");

            migrationBuilder.CreateIndex(
                name: "IX_Contatc_UserId",
                table: "Contatc",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_User_UserId",
                table: "Contact",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Users_Create_And_Update_User_DTOId",
                table: "Contact",
                column: "Create_And_Update_User_DTOId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_User_UserId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Users_Create_And_Update_User_DTOId",
                table: "Contact");

            migrationBuilder.DropTable(
                name: "Contatc");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Contact_Create_And_Update_User_DTOId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "Create_And_Update_User_DTOId",
                table: "Contact");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Users_UserId",
                table: "Contact",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
