using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agenda_api.Migrations
{
    public partial class initial_fav : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    Rol = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CelularNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    TelephoneNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Favorite = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "LastName", "Name", "Password", "Rol", "State", "UserName" },
                values: new object[] { 1, "karenbailapiola@gmail.com", "Lasot", "Karen", "Pa$$w0rd", 0, 0, "karenpiola" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "LastName", "Name", "Password", "Rol", "State", "UserName" },
                values: new object[] { 2, "elluismidetotoras@gmail.com", "Gonzales", "Luis Gonzalez", "lamismadesiempre", 1, 0, "luismitoto" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CelularNumber", "Favorite", "Name", "TelephoneNumber", "UserId" },
                values: new object[] { 1, 341457896, false, "Jaimito", null, 1 });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CelularNumber", "Favorite", "Name", "TelephoneNumber", "UserId" },
                values: new object[] { 2, 34156978, false, "Pepe", 422568, 2 });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CelularNumber", "Favorite", "Name", "TelephoneNumber", "UserId" },
                values: new object[] { 3, 11425789, false, "Maria", null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_UserId",
                table: "Contacts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
