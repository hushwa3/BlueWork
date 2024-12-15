using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueWork.web.Migrations.BlueWorkDb
{
    /// <inheritdoc />
    public partial class updatechanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerProfiles_ApplicationUser_UserId",
                table: "WorkerProfiles");

            migrationBuilder.DropIndex(
                name: "IX_WorkerProfiles_UserId",
                table: "WorkerProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "WorkerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "WorkerProfiles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerProfiles_UserId",
                table: "WorkerProfiles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerProfiles_ApplicationUser_UserId",
                table: "WorkerProfiles",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
