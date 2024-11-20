using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueWork.web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRegister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Registrations",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Registrations",
                newName: "Industry");

            migrationBuilder.AddColumn<string>(
                name: "CompanyDescription",
                table: "Registrations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Registrations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "ProfilePicture",
                table: "Registrations",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "ContactNumber",
                table: "EmployerProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "EmployerProfiles",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "EmployerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "EmployerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "EmployerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyDescription",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "EmployerProfiles");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "EmployerProfiles");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "EmployerProfiles");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "EmployerProfiles");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "EmployerProfiles");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Registrations",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Industry",
                table: "Registrations",
                newName: "Address");
        }
    }
}
