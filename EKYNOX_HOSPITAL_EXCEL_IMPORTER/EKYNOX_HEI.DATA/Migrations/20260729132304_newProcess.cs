using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EKYNOX_HEI.DATA.Migrations
{
    /// <inheritdoc />
    public partial class newProcess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CREATEDATE",
                table: "Institutions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CREATEDUSER",
                table: "Institutions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "MODIFIEDDATE",
                table: "Institutions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MODIFIEDUSER",
                table: "Institutions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CREATEDATE",
                table: "EducationAttendanceFileRead",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CREATEDUSER",
                table: "EducationAttendanceFileRead",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "MODIFIEDDATE",
                table: "EducationAttendanceFileRead",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MODIFIEDUSER",
                table: "EducationAttendanceFileRead",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CREATEDATE",
                table: "EducationAttendanceDetail",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CREATEDUSER",
                table: "EducationAttendanceDetail",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "MODIFIEDDATE",
                table: "EducationAttendanceDetail",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MODIFIEDUSER",
                table: "EducationAttendanceDetail",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CREATEDATE",
                table: "EducationAttendance",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CREATEDUSER",
                table: "EducationAttendance",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "MODIFIEDDATE",
                table: "EducationAttendance",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MODIFIEDUSER",
                table: "EducationAttendance",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CREATEDATE",
                table: "AISettingDetail",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CREATEDUSER",
                table: "AISettingDetail",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "MODIFIEDDATE",
                table: "AISettingDetail",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MODIFIEDUSER",
                table: "AISettingDetail",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AINO",
                table: "AISetting",
                type: "TEXT",
                maxLength: 51,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CREATEDATE",
                table: "AISetting",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CREATEDUSER",
                table: "AISetting",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "MODIFIEDDATE",
                table: "AISetting",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MODIFIEDUSER",
                table: "AISetting",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CREATEDATE",
                table: "Institutions");

            migrationBuilder.DropColumn(
                name: "CREATEDUSER",
                table: "Institutions");

            migrationBuilder.DropColumn(
                name: "MODIFIEDDATE",
                table: "Institutions");

            migrationBuilder.DropColumn(
                name: "MODIFIEDUSER",
                table: "Institutions");

            migrationBuilder.DropColumn(
                name: "CREATEDATE",
                table: "EducationAttendanceFileRead");

            migrationBuilder.DropColumn(
                name: "CREATEDUSER",
                table: "EducationAttendanceFileRead");

            migrationBuilder.DropColumn(
                name: "MODIFIEDDATE",
                table: "EducationAttendanceFileRead");

            migrationBuilder.DropColumn(
                name: "MODIFIEDUSER",
                table: "EducationAttendanceFileRead");

            migrationBuilder.DropColumn(
                name: "CREATEDATE",
                table: "EducationAttendanceDetail");

            migrationBuilder.DropColumn(
                name: "CREATEDUSER",
                table: "EducationAttendanceDetail");

            migrationBuilder.DropColumn(
                name: "MODIFIEDDATE",
                table: "EducationAttendanceDetail");

            migrationBuilder.DropColumn(
                name: "MODIFIEDUSER",
                table: "EducationAttendanceDetail");

            migrationBuilder.DropColumn(
                name: "CREATEDATE",
                table: "EducationAttendance");

            migrationBuilder.DropColumn(
                name: "CREATEDUSER",
                table: "EducationAttendance");

            migrationBuilder.DropColumn(
                name: "MODIFIEDDATE",
                table: "EducationAttendance");

            migrationBuilder.DropColumn(
                name: "MODIFIEDUSER",
                table: "EducationAttendance");

            migrationBuilder.DropColumn(
                name: "CREATEDATE",
                table: "AISettingDetail");

            migrationBuilder.DropColumn(
                name: "CREATEDUSER",
                table: "AISettingDetail");

            migrationBuilder.DropColumn(
                name: "MODIFIEDDATE",
                table: "AISettingDetail");

            migrationBuilder.DropColumn(
                name: "MODIFIEDUSER",
                table: "AISettingDetail");

            migrationBuilder.DropColumn(
                name: "AINO",
                table: "AISetting");

            migrationBuilder.DropColumn(
                name: "CREATEDATE",
                table: "AISetting");

            migrationBuilder.DropColumn(
                name: "CREATEDUSER",
                table: "AISetting");

            migrationBuilder.DropColumn(
                name: "MODIFIEDDATE",
                table: "AISetting");

            migrationBuilder.DropColumn(
                name: "MODIFIEDUSER",
                table: "AISetting");
        }
    }
}
