﻿// <auto-generated />
using System;
using BlueWork.web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlueWork.web.Migrations.BlueWorkDb
{
    [DbContext(typeof(BlueWorkDbContext))]
    [Migration("20241215160448_AddClientProfile")]
    partial class AddClientProfile
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BlueWork.web.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUser");
                });

            modelBuilder.Entity("BlueWork.web.Models.EmployerProfile", b =>
                {
                    b.Property<int>("EmployerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployerID"));

                    b.Property<string>("CompanyDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ContactNumber")
                        .HasColumnType("int");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Industry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserAccountId")
                        .HasColumnType("int");

                    b.HasKey("EmployerID");

                    b.ToTable("EmployerProfiles");
                });

            modelBuilder.Entity("BlueWork.web.Models.JobApplication", b =>
                {
                    b.Property<int>("ApplicationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ApplicationID"));

                    b.Property<DateTime>("ApplicationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ApplicationDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Industry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("JobID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkerID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ApplicationID");

                    b.ToTable("JobApplications");
                });

            modelBuilder.Entity("BlueWork.web.Models.JobListing", b =>
                {
                    b.Property<int>("JobID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobID"));

                    b.Property<decimal>("BudgetFrom")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BudgetTo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Complexity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployerID")
                        .HasColumnType("int");

                    b.Property<string>("Experience")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("MaximumBudget")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RequiredSkills")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("JobID");

                    b.ToTable("JobListings");
                });

            modelBuilder.Entity("BlueWork.web.Models.JobPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Complexity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Experience")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Headline")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("HourlyRateFrom")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("HourlyRateTo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ProjectBudget")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Skills")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("JobPosts");
                });

            modelBuilder.Entity("BlueWork.web.Models.SkillDevelopment", b =>
                {
                    b.Property<int>("SkillID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SkillID"));

                    b.Property<int>("EmployerID")
                        .HasColumnType("int");

                    b.Property<string>("SkillDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SkillName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrainingDuration")
                        .HasColumnType("int");

                    b.Property<string>("TrainingProvider")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkerID")
                        .HasColumnType("int");

                    b.HasKey("SkillID");

                    b.ToTable("SkillDevelopments");
                });

            modelBuilder.Entity("BlueWork.web.Models.WorkerProfile", b =>
                {
                    b.Property<int>("WorkerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WorkerID"));

                    b.Property<string>("Education")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HoursPerWeek")
                        .HasColumnType("int");

                    b.Property<int?>("JobApplicationApplicationID")
                        .HasColumnType("int");

                    b.Property<string>("Languages")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocationCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Offer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SkillDevelopmentSkillID")
                        .HasColumnType("int");

                    b.Property<string>("Speciality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("course")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("responseTime")
                        .HasColumnType("int");

                    b.Property<int>("year")
                        .HasColumnType("int");

                    b.HasKey("WorkerID");

                    b.HasIndex("JobApplicationApplicationID");

                    b.HasIndex("SkillDevelopmentSkillID");

                    b.HasIndex("UserId");

                    b.ToTable("WorkerProfiles");
                });

            modelBuilder.Entity("EmployerProfileJobListing", b =>
                {
                    b.Property<int>("EmployerProfilesEmployerID")
                        .HasColumnType("int");

                    b.Property<int>("JobListingsJobID")
                        .HasColumnType("int");

                    b.HasKey("EmployerProfilesEmployerID", "JobListingsJobID");

                    b.HasIndex("JobListingsJobID");

                    b.ToTable("EmployerProfileJobListing");
                });

            modelBuilder.Entity("EmployerProfileSkillDevelopment", b =>
                {
                    b.Property<int>("EmployerProfilesEmployerID")
                        .HasColumnType("int");

                    b.Property<int>("SkillDevelopmentSkillID")
                        .HasColumnType("int");

                    b.HasKey("EmployerProfilesEmployerID", "SkillDevelopmentSkillID");

                    b.HasIndex("SkillDevelopmentSkillID");

                    b.ToTable("EmployerProfileSkillDevelopment");
                });

            modelBuilder.Entity("JobApplicationJobListing", b =>
                {
                    b.Property<int>("JobApplicationsApplicationID")
                        .HasColumnType("int");

                    b.Property<int>("JobListingsJobID")
                        .HasColumnType("int");

                    b.HasKey("JobApplicationsApplicationID", "JobListingsJobID");

                    b.HasIndex("JobListingsJobID");

                    b.ToTable("JobApplicationJobListing");
                });

            modelBuilder.Entity("BlueWork.web.Models.JobPost", b =>
                {
                    b.HasOne("BlueWork.web.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BlueWork.web.Models.WorkerProfile", b =>
                {
                    b.HasOne("BlueWork.web.Models.JobApplication", null)
                        .WithMany("WorkerProfiles")
                        .HasForeignKey("JobApplicationApplicationID");

                    b.HasOne("BlueWork.web.Models.SkillDevelopment", null)
                        .WithMany("WorkerProfiles")
                        .HasForeignKey("SkillDevelopmentSkillID");

                    b.HasOne("BlueWork.web.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EmployerProfileJobListing", b =>
                {
                    b.HasOne("BlueWork.web.Models.EmployerProfile", null)
                        .WithMany()
                        .HasForeignKey("EmployerProfilesEmployerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlueWork.web.Models.JobListing", null)
                        .WithMany()
                        .HasForeignKey("JobListingsJobID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EmployerProfileSkillDevelopment", b =>
                {
                    b.HasOne("BlueWork.web.Models.EmployerProfile", null)
                        .WithMany()
                        .HasForeignKey("EmployerProfilesEmployerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlueWork.web.Models.SkillDevelopment", null)
                        .WithMany()
                        .HasForeignKey("SkillDevelopmentSkillID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JobApplicationJobListing", b =>
                {
                    b.HasOne("BlueWork.web.Models.JobApplication", null)
                        .WithMany()
                        .HasForeignKey("JobApplicationsApplicationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlueWork.web.Models.JobListing", null)
                        .WithMany()
                        .HasForeignKey("JobListingsJobID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BlueWork.web.Models.JobApplication", b =>
                {
                    b.Navigation("WorkerProfiles");
                });

            modelBuilder.Entity("BlueWork.web.Models.SkillDevelopment", b =>
                {
                    b.Navigation("WorkerProfiles");
                });
#pragma warning restore 612, 618
        }
    }
}
