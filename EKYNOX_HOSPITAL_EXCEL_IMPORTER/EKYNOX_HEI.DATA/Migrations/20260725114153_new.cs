using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EKYNOX_HEI.DATA.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EDUCATIONSTATUS",
                table: "EducationAttendance",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EDUCATIONSTATUS",
                table: "EducationAttendance");
        }
    }
}
