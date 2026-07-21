using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EKYNOX_HEI.DATA.Migrations
{
    /// <inheritdoc />
    public partial class newTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EducationAttendance",
                columns: table => new
                {
                    LOGICALREF = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    INSTUTIONREF = table.Column<int>(type: "INTEGER", nullable: false),
                    DOCNO = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    EDUCATIONFULLNAME = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    DATE_ = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FILEPATH = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    FILENAME = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationAttendance", x => x.LOGICALREF);
                });

            migrationBuilder.CreateTable(
                name: "Institutions",
                columns: table => new
                {
                    LOGICALREF = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CODE = table.Column<string>(type: "TEXT", maxLength: 51, nullable: true),
                    NAME = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    CITY = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    TOWN = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    DISTRICT = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ADDRESS = table.Column<string>(type: "TEXT", maxLength: 700, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutions", x => x.LOGICALREF);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationAttendance");

            migrationBuilder.DropTable(
                name: "Institutions");
        }
    }
}
