using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EKYNOX_HEI.DATA.Migrations
{
    /// <inheritdoc />
    public partial class educatiınAttendanceDetail_addFileMimeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FILEMIMETYPE",
                table: "EducationAttendanceDetail",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FILEMIMETYPE",
                table: "EducationAttendanceDetail");
        }
    }
}
