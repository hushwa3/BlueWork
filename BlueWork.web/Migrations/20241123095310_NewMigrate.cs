using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueWork.web.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployerProfileLogin");

            migrationBuilder.DropTable(
                name: "EmployerProfileRegistration");

            migrationBuilder.DropTable(
                name: "LoginWorkerProfile");

            migrationBuilder.DropTable(
                name: "RegistrationWorkerProfile");

            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "WorkerProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployerProfileEmployerID",
                table: "Logins",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkerProfileWorkerID",
                table: "Logins",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "EmployerProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AbpUserAccounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    UserLinkId = table.Column<long>(type: "bigint", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmployerProfileEmployerID = table.Column<int>(type: "int", nullable: true),
                    WorkerProfileWorkerID = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpUserAccounts_EmployerProfiles_EmployerProfileEmployerID",
                        column: x => x.EmployerProfileEmployerID,
                        principalTable: "EmployerProfiles",
                        principalColumn: "EmployerID");
                    table.ForeignKey(
                        name: "FK_AbpUserAccounts_WorkerProfiles_WorkerProfileWorkerID",
                        column: x => x.WorkerProfileWorkerID,
                        principalTable: "WorkerProfiles",
                        principalColumn: "WorkerID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logins_EmployerProfileEmployerID",
                table: "Logins",
                column: "EmployerProfileEmployerID");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_WorkerProfileWorkerID",
                table: "Logins",
                column: "WorkerProfileWorkerID");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserAccounts_EmployerProfileEmployerID",
                table: "AbpUserAccounts",
                column: "EmployerProfileEmployerID");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserAccounts_WorkerProfileWorkerID",
                table: "AbpUserAccounts",
                column: "WorkerProfileWorkerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_EmployerProfiles_EmployerProfileEmployerID",
                table: "Logins",
                column: "EmployerProfileEmployerID",
                principalTable: "EmployerProfiles",
                principalColumn: "EmployerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_WorkerProfiles_WorkerProfileWorkerID",
                table: "Logins",
                column: "WorkerProfileWorkerID",
                principalTable: "WorkerProfiles",
                principalColumn: "WorkerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_EmployerProfiles_EmployerProfileEmployerID",
                table: "Logins");

            migrationBuilder.DropForeignKey(
                name: "FK_Logins_WorkerProfiles_WorkerProfileWorkerID",
                table: "Logins");

            migrationBuilder.DropTable(
                name: "AbpUserAccounts");

            migrationBuilder.DropIndex(
                name: "IX_Logins_EmployerProfileEmployerID",
                table: "Logins");

            migrationBuilder.DropIndex(
                name: "IX_Logins_WorkerProfileWorkerID",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "WorkerProfiles");

            migrationBuilder.DropColumn(
                name: "EmployerProfileEmployerID",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "WorkerProfileWorkerID",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "EmployerProfiles");

            migrationBuilder.CreateTable(
                name: "EmployerProfileLogin",
                columns: table => new
                {
                    EmployerProfilesEmployerID = table.Column<int>(type: "int", nullable: false),
                    LoginID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerProfileLogin", x => new { x.EmployerProfilesEmployerID, x.LoginID });
                    table.ForeignKey(
                        name: "FK_EmployerProfileLogin_EmployerProfiles_EmployerProfilesEmployerID",
                        column: x => x.EmployerProfilesEmployerID,
                        principalTable: "EmployerProfiles",
                        principalColumn: "EmployerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployerProfileLogin_Logins_LoginID",
                        column: x => x.LoginID,
                        principalTable: "Logins",
                        principalColumn: "LoginID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployerProfileRegistration",
                columns: table => new
                {
                    EmployerProfilesEmployerID = table.Column<int>(type: "int", nullable: false),
                    RegistrationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerProfileRegistration", x => new { x.EmployerProfilesEmployerID, x.RegistrationID });
                    table.ForeignKey(
                        name: "FK_EmployerProfileRegistration_EmployerProfiles_EmployerProfilesEmployerID",
                        column: x => x.EmployerProfilesEmployerID,
                        principalTable: "EmployerProfiles",
                        principalColumn: "EmployerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployerProfileRegistration_Registrations_RegistrationID",
                        column: x => x.RegistrationID,
                        principalTable: "Registrations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoginWorkerProfile",
                columns: table => new
                {
                    LoginsLoginID = table.Column<int>(type: "int", nullable: false),
                    WorkerProfilesWorkerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginWorkerProfile", x => new { x.LoginsLoginID, x.WorkerProfilesWorkerID });
                    table.ForeignKey(
                        name: "FK_LoginWorkerProfile_Logins_LoginsLoginID",
                        column: x => x.LoginsLoginID,
                        principalTable: "Logins",
                        principalColumn: "LoginID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoginWorkerProfile_WorkerProfiles_WorkerProfilesWorkerID",
                        column: x => x.WorkerProfilesWorkerID,
                        principalTable: "WorkerProfiles",
                        principalColumn: "WorkerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationWorkerProfile",
                columns: table => new
                {
                    RegistrationsID = table.Column<int>(type: "int", nullable: false),
                    WorkerProfilesWorkerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationWorkerProfile", x => new { x.RegistrationsID, x.WorkerProfilesWorkerID });
                    table.ForeignKey(
                        name: "FK_RegistrationWorkerProfile_Registrations_RegistrationsID",
                        column: x => x.RegistrationsID,
                        principalTable: "Registrations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistrationWorkerProfile_WorkerProfiles_WorkerProfilesWorkerID",
                        column: x => x.WorkerProfilesWorkerID,
                        principalTable: "WorkerProfiles",
                        principalColumn: "WorkerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployerProfileLogin_LoginID",
                table: "EmployerProfileLogin",
                column: "LoginID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerProfileRegistration_RegistrationID",
                table: "EmployerProfileRegistration",
                column: "RegistrationID");

            migrationBuilder.CreateIndex(
                name: "IX_LoginWorkerProfile_WorkerProfilesWorkerID",
                table: "LoginWorkerProfile",
                column: "WorkerProfilesWorkerID");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationWorkerProfile_WorkerProfilesWorkerID",
                table: "RegistrationWorkerProfile",
                column: "WorkerProfilesWorkerID");
        }
    }
}
