using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EKYNOX_HEI.DATA.Migrations
{
    /// <inheritdoc />
    public partial class educationAttrEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DATE_",
                table: "EducationAttendance");

            migrationBuilder.DropColumn(
                name: "EDUCATIONFULLNAME",
                table: "EducationAttendance");

            migrationBuilder.DropColumn(
                name: "FILENAME",
                table: "EducationAttendance");

            migrationBuilder.DropColumn(
                name: "FILEPATH",
                table: "EducationAttendance");

            migrationBuilder.AddColumn<int>(
                name: "EDUCATORREF",
                table: "EducationAttendance",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EducationAttendanceDetail",
                columns: table => new
                {
                    LOGICALREF = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EDUCATIONATTENDANCEREF = table.Column<int>(type: "INTEGER", nullable: false),
                    FILENAME = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    FILEPATH = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    FILEDATA = table.Column<byte[]>(type: "BLOB", nullable: true),
                    EDUCATIONDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EDUCATIONTYPE = table.Column<int>(type: "INTEGER", nullable: false),
                    MODULETYPE = table.Column<int>(type: "INTEGER", nullable: false),
                    EDUCATIONNUMBER = table.Column<int>(type: "INTEGER", nullable: false),
                    READANDEXCELPROCESS = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationAttendanceDetail", x => x.LOGICALREF);
                });

            migrationBuilder.CreateTable(
                name: "EducationAttendanceFileRead",
                columns: table => new
                {
                    LOGICALREF = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EDUCATIONATTENDANCEDETAILREF = table.Column<int>(type: "INTEGER", nullable: false),
                    CLASSNO = table.Column<int>(type: "INTEGER", nullable: false),
                    NAME = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    SURNAME = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationAttendanceFileRead", x => x.LOGICALREF);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationAttendanceDetail");

            migrationBuilder.DropTable(
                name: "EducationAttendanceFileRead");

            migrationBuilder.DropColumn(
                name: "EDUCATORREF",
                table: "EducationAttendance");

            migrationBuilder.AddColumn<DateTime>(
                name: "DATE_",
                table: "EducationAttendance",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EDUCATIONFULLNAME",
                table: "EducationAttendance",
                type: "TEXT",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FILENAME",
                table: "EducationAttendance",
                type: "TEXT",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FILEPATH",
                table: "EducationAttendance",
                type: "TEXT",
                maxLength: 300,
                nullable: true);
        }
    }
}
