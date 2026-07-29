using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EKYNOX_HEI.DATA.Migrations
{
    /// <inheritdoc />
    public partial class AiSettingDetail_addLineNrColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LINENR",
                table: "AISettingDetail",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LINENR",
                table: "AISettingDetail");
        }
    }
}
