using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueWork.web.Migrations.BlueWorkDb
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobApplicationWorkerProfile");

            migrationBuilder.DropTable(
                name: "SkillDevelopmentWorkerProfile");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "WorkerProfiles");

            migrationBuilder.RenameColumn(
                name: "UserAccountId",
                table: "WorkerProfiles",
                newName: "year");

            migrationBuilder.RenameColumn(
                name: "Skills",
                table: "WorkerProfiles",
                newName: "course");

            migrationBuilder.RenameColumn(
                name: "ExperienceYears",
                table: "WorkerProfiles",
                newName: "responseTime");

            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "WorkerProfiles",
                newName: "Speciality");

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "WorkerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "HoursPerWeek",
                table: "WorkerProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JobApplicationApplicationID",
                table: "WorkerProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                table: "WorkerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LocationCity",
                table: "WorkerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Offer",
                table: "WorkerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SkillDevelopmentSkillID",
                table: "WorkerProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "WorkerProfiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerProfiles_JobApplicationApplicationID",
                table: "WorkerProfiles",
                column: "JobApplicationApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerProfiles_SkillDevelopmentSkillID",
                table: "WorkerProfiles",
                column: "SkillDevelopmentSkillID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerProfiles_JobApplications_JobApplicationApplicationID",
                table: "WorkerProfiles",
                column: "JobApplicationApplicationID",
                principalTable: "JobApplications",
                principalColumn: "ApplicationID");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerProfiles_SkillDevelopments_SkillDevelopmentSkillID",
                table: "WorkerProfiles",
                column: "SkillDevelopmentSkillID",
                principalTable: "SkillDevelopments",
                principalColumn: "SkillID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerProfiles_ApplicationUser_UserId",
                table: "WorkerProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerProfiles_JobApplications_JobApplicationApplicationID",
                table: "WorkerProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerProfiles_SkillDevelopments_SkillDevelopmentSkillID",
                table: "WorkerProfiles");

            migrationBuilder.DropIndex(
                name: "IX_WorkerProfiles_JobApplicationApplicationID",
                table: "WorkerProfiles");

            migrationBuilder.DropIndex(
                name: "IX_WorkerProfiles_SkillDevelopmentSkillID",
                table: "WorkerProfiles");

            migrationBuilder.DropIndex(
                name: "IX_WorkerProfiles_UserId",
                table: "WorkerProfiles");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "WorkerProfiles");

            migrationBuilder.DropColumn(
                name: "HoursPerWeek",
                table: "WorkerProfiles");

            migrationBuilder.DropColumn(
                name: "JobApplicationApplicationID",
                table: "WorkerProfiles");

            migrationBuilder.DropColumn(
                name: "Languages",
                table: "WorkerProfiles");

            migrationBuilder.DropColumn(
                name: "LocationCity",
                table: "WorkerProfiles");

            migrationBuilder.DropColumn(
                name: "Offer",
                table: "WorkerProfiles");

            migrationBuilder.DropColumn(
                name: "SkillDevelopmentSkillID",
                table: "WorkerProfiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WorkerProfiles");

            migrationBuilder.RenameColumn(
                name: "year",
                table: "WorkerProfiles",
                newName: "UserAccountId");

            migrationBuilder.RenameColumn(
                name: "responseTime",
                table: "WorkerProfiles",
                newName: "ExperienceYears");

            migrationBuilder.RenameColumn(
                name: "course",
                table: "WorkerProfiles",
                newName: "Skills");

            migrationBuilder.RenameColumn(
                name: "Speciality",
                table: "WorkerProfiles",
                newName: "Bio");

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "WorkerProfiles",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "JobApplicationWorkerProfile",
                columns: table => new
                {
                    JobApplicationsApplicationID = table.Column<int>(type: "int", nullable: false),
                    WorkerProfilesWorkerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplicationWorkerProfile", x => new { x.JobApplicationsApplicationID, x.WorkerProfilesWorkerID });
                    table.ForeignKey(
                        name: "FK_JobApplicationWorkerProfile_JobApplications_JobApplicationsApplicationID",
                        column: x => x.JobApplicationsApplicationID,
                        principalTable: "JobApplications",
                        principalColumn: "ApplicationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobApplicationWorkerProfile_WorkerProfiles_WorkerProfilesWorkerID",
                        column: x => x.WorkerProfilesWorkerID,
                        principalTable: "WorkerProfiles",
                        principalColumn: "WorkerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkillDevelopmentWorkerProfile",
                columns: table => new
                {
                    SkillDevelopmentsSkillID = table.Column<int>(type: "int", nullable: false),
                    WorkerProfilesWorkerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillDevelopmentWorkerProfile", x => new { x.SkillDevelopmentsSkillID, x.WorkerProfilesWorkerID });
                    table.ForeignKey(
                        name: "FK_SkillDevelopmentWorkerProfile_SkillDevelopments_SkillDevelopmentsSkillID",
                        column: x => x.SkillDevelopmentsSkillID,
                        principalTable: "SkillDevelopments",
                        principalColumn: "SkillID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillDevelopmentWorkerProfile_WorkerProfiles_WorkerProfilesWorkerID",
                        column: x => x.WorkerProfilesWorkerID,
                        principalTable: "WorkerProfiles",
                        principalColumn: "WorkerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicationWorkerProfile_WorkerProfilesWorkerID",
                table: "JobApplicationWorkerProfile",
                column: "WorkerProfilesWorkerID");

            migrationBuilder.CreateIndex(
                name: "IX_SkillDevelopmentWorkerProfile_WorkerProfilesWorkerID",
                table: "SkillDevelopmentWorkerProfile",
                column: "WorkerProfilesWorkerID");
        }
    }
}
