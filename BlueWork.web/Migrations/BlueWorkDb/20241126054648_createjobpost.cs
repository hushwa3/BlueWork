using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueWork.web.Migrations.BlueWorkDb
{
    /// <inheritdoc />
    public partial class createjobpost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployerProfiles",
                columns: table => new
                {
                    EmployerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAccountId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Industry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerProfiles", x => x.EmployerID);
                });

            migrationBuilder.CreateTable(
                name: "JobApplications",
                columns: table => new
                {
                    ApplicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Industry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobID = table.Column<int>(type: "int", nullable: false),
                    WorkerID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.ApplicationID);
                });

            migrationBuilder.CreateTable(
                name: "JobListings",
                columns: table => new
                {
                    JobID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredSkills = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Complexity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudgetFrom = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BudgetTo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaximumBudget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobListings", x => x.JobID);
                });

            migrationBuilder.CreateTable(
                name: "JobPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Headline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Complexity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HourlyRateFrom = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HourlyRateTo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProjectBudget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkillDevelopments",
                columns: table => new
                {
                    SkillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrainingProvider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrainingDuration = table.Column<int>(type: "int", nullable: false),
                    WorkerID = table.Column<int>(type: "int", nullable: false),
                    EmployerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillDevelopments", x => x.SkillID);
                });

            migrationBuilder.CreateTable(
                name: "WorkerProfiles",
                columns: table => new
                {
                    WorkerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAccountId = table.Column<int>(type: "int", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerProfiles", x => x.WorkerID);
                });

            migrationBuilder.CreateTable(
                name: "EmployerProfileJobListing",
                columns: table => new
                {
                    EmployerProfilesEmployerID = table.Column<int>(type: "int", nullable: false),
                    JobListingsJobID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerProfileJobListing", x => new { x.EmployerProfilesEmployerID, x.JobListingsJobID });
                    table.ForeignKey(
                        name: "FK_EmployerProfileJobListing_EmployerProfiles_EmployerProfilesEmployerID",
                        column: x => x.EmployerProfilesEmployerID,
                        principalTable: "EmployerProfiles",
                        principalColumn: "EmployerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployerProfileJobListing_JobListings_JobListingsJobID",
                        column: x => x.JobListingsJobID,
                        principalTable: "JobListings",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobApplicationJobListing",
                columns: table => new
                {
                    JobApplicationsApplicationID = table.Column<int>(type: "int", nullable: false),
                    JobListingsJobID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplicationJobListing", x => new { x.JobApplicationsApplicationID, x.JobListingsJobID });
                    table.ForeignKey(
                        name: "FK_JobApplicationJobListing_JobApplications_JobApplicationsApplicationID",
                        column: x => x.JobApplicationsApplicationID,
                        principalTable: "JobApplications",
                        principalColumn: "ApplicationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobApplicationJobListing_JobListings_JobListingsJobID",
                        column: x => x.JobListingsJobID,
                        principalTable: "JobListings",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployerProfileSkillDevelopment",
                columns: table => new
                {
                    EmployerProfilesEmployerID = table.Column<int>(type: "int", nullable: false),
                    SkillDevelopmentSkillID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerProfileSkillDevelopment", x => new { x.EmployerProfilesEmployerID, x.SkillDevelopmentSkillID });
                    table.ForeignKey(
                        name: "FK_EmployerProfileSkillDevelopment_EmployerProfiles_EmployerProfilesEmployerID",
                        column: x => x.EmployerProfilesEmployerID,
                        principalTable: "EmployerProfiles",
                        principalColumn: "EmployerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployerProfileSkillDevelopment_SkillDevelopments_SkillDevelopmentSkillID",
                        column: x => x.SkillDevelopmentSkillID,
                        principalTable: "SkillDevelopments",
                        principalColumn: "SkillID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_EmployerProfileJobListing_JobListingsJobID",
                table: "EmployerProfileJobListing",
                column: "JobListingsJobID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerProfileSkillDevelopment_SkillDevelopmentSkillID",
                table: "EmployerProfileSkillDevelopment",
                column: "SkillDevelopmentSkillID");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicationJobListing_JobListingsJobID",
                table: "JobApplicationJobListing",
                column: "JobListingsJobID");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicationWorkerProfile_WorkerProfilesWorkerID",
                table: "JobApplicationWorkerProfile",
                column: "WorkerProfilesWorkerID");

            migrationBuilder.CreateIndex(
                name: "IX_SkillDevelopmentWorkerProfile_WorkerProfilesWorkerID",
                table: "SkillDevelopmentWorkerProfile",
                column: "WorkerProfilesWorkerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployerProfileJobListing");

            migrationBuilder.DropTable(
                name: "EmployerProfileSkillDevelopment");

            migrationBuilder.DropTable(
                name: "JobApplicationJobListing");

            migrationBuilder.DropTable(
                name: "JobApplicationWorkerProfile");

            migrationBuilder.DropTable(
                name: "JobPosts");

            migrationBuilder.DropTable(
                name: "SkillDevelopmentWorkerProfile");

            migrationBuilder.DropTable(
                name: "EmployerProfiles");

            migrationBuilder.DropTable(
                name: "JobListings");

            migrationBuilder.DropTable(
                name: "JobApplications");

            migrationBuilder.DropTable(
                name: "SkillDevelopments");

            migrationBuilder.DropTable(
                name: "WorkerProfiles");
        }
    }
}
