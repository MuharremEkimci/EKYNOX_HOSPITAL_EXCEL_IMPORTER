using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EKYNOX_HEI.DATA.Migrations
{
    /// <inheritdoc />
    public partial class AISetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AISetting",
                columns: table => new
                {
                    LOGICALREF = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AI = table.Column<int>(type: "INTEGER", nullable: false),
                    APIKEY = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    USINGSTATUS = table.Column<int>(type: "INTEGER", nullable: false),
                    METHODNAME = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    ENDPOINT = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AISetting", x => x.LOGICALREF);
                });

            migrationBuilder.CreateTable(
                name: "AISettingDetail",
                columns: table => new
                {
                    LOGICALREF = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AISETTINGREF = table.Column<int>(type: "INTEGER", nullable: false),
                    AIMODELNAME = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    AIMODELDESC = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AISettingDetail", x => x.LOGICALREF);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AISetting");

            migrationBuilder.DropTable(
                name: "AISettingDetail");
        }
    }
}
