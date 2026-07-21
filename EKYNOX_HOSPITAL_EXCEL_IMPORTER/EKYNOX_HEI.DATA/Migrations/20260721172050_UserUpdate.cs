using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EKYNOX_HEI.DATA.Migrations
{
    /// <inheritdoc />
    public partial class UserUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    LOGICALREF = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NR = table.Column<int>(type: "INTEGER", nullable: false),
                    USERNAME = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    NAME = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    SURNAME = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    PASSWORD = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    EMAIL = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    PHONE = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    ROLE = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.LOGICALREF);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
