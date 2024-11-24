using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueWork.web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginRegistration");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "UserAccounts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_RegistrationID",
                table: "Logins",
                column: "RegistrationID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Registrations_RegistrationID",
                table: "Logins",
                column: "RegistrationID",
                principalTable: "Registrations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Registrations_RegistrationID",
                table: "Logins");

            migrationBuilder.DropIndex(
                name: "IX_Logins_RegistrationID",
                table: "Logins");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserAccounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "LoginRegistration",
                columns: table => new
                {
                    LoginsLoginID = table.Column<int>(type: "int", nullable: false),
                    RegistrationsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginRegistration", x => new { x.LoginsLoginID, x.RegistrationsID });
                    table.ForeignKey(
                        name: "FK_LoginRegistration_Logins_LoginsLoginID",
                        column: x => x.LoginsLoginID,
                        principalTable: "Logins",
                        principalColumn: "LoginID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoginRegistration_Registrations_RegistrationsID",
                        column: x => x.RegistrationsID,
                        principalTable: "Registrations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoginRegistration_RegistrationsID",
                table: "LoginRegistration",
                column: "RegistrationsID");
        }
    }
}
