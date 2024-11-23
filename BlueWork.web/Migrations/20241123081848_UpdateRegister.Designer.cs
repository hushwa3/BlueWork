﻿// <auto-generated />
using System;
using BlueWork.web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlueWork.web.Migrations
{
    [DbContext(typeof(BlueWorkDbContext))]
    [Migration("20241123081848_UpdateRegister")]
    partial class UpdateRegister
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BlueWork.web.BlueWorkAuth.UserAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("UserAccounts");
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

                    b.Property<int>("RegistrationID")
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

            modelBuilder.Entity("BlueWork.web.Models.Login", b =>
                {
                    b.Property<int>("LoginID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoginID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("RegistrationID")
                        .HasColumnType("int");

                    b.HasKey("LoginID");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("BlueWork.web.Models.Registration", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Registrations");
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

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExperienceYears")
                        .HasColumnType("int");

                    b.Property<decimal>("Rating")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RegistrationID")
                        .HasColumnType("int");

                    b.Property<string>("Skills")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WorkerID");

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

            modelBuilder.Entity("EmployerProfileLogin", b =>
                {
                    b.Property<int>("EmployerProfilesEmployerID")
                        .HasColumnType("int");

                    b.Property<int>("LoginID")
                        .HasColumnType("int");

                    b.HasKey("EmployerProfilesEmployerID", "LoginID");

                    b.HasIndex("LoginID");

                    b.ToTable("EmployerProfileLogin");
                });

            modelBuilder.Entity("EmployerProfileRegistration", b =>
                {
                    b.Property<int>("EmployerProfilesEmployerID")
                        .HasColumnType("int");

                    b.Property<int>("RegistrationID")
                        .HasColumnType("int");

                    b.HasKey("EmployerProfilesEmployerID", "RegistrationID");

                    b.HasIndex("RegistrationID");

                    b.ToTable("EmployerProfileRegistration");
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

            modelBuilder.Entity("JobApplicationWorkerProfile", b =>
                {
                    b.Property<int>("JobApplicationsApplicationID")
                        .HasColumnType("int");

                    b.Property<int>("WorkerProfilesWorkerID")
                        .HasColumnType("int");

                    b.HasKey("JobApplicationsApplicationID", "WorkerProfilesWorkerID");

                    b.HasIndex("WorkerProfilesWorkerID");

                    b.ToTable("JobApplicationWorkerProfile");
                });

            modelBuilder.Entity("LoginRegistration", b =>
                {
                    b.Property<int>("LoginsLoginID")
                        .HasColumnType("int");

                    b.Property<int>("RegistrationsID")
                        .HasColumnType("int");

                    b.HasKey("LoginsLoginID", "RegistrationsID");

                    b.HasIndex("RegistrationsID");

                    b.ToTable("LoginRegistration");
                });

            modelBuilder.Entity("LoginWorkerProfile", b =>
                {
                    b.Property<int>("LoginsLoginID")
                        .HasColumnType("int");

                    b.Property<int>("WorkerProfilesWorkerID")
                        .HasColumnType("int");

                    b.HasKey("LoginsLoginID", "WorkerProfilesWorkerID");

                    b.HasIndex("WorkerProfilesWorkerID");

                    b.ToTable("LoginWorkerProfile");
                });

            modelBuilder.Entity("RegistrationWorkerProfile", b =>
                {
                    b.Property<int>("RegistrationsID")
                        .HasColumnType("int");

                    b.Property<int>("WorkerProfilesWorkerID")
                        .HasColumnType("int");

                    b.HasKey("RegistrationsID", "WorkerProfilesWorkerID");

                    b.HasIndex("WorkerProfilesWorkerID");

                    b.ToTable("RegistrationWorkerProfile");
                });

            modelBuilder.Entity("SkillDevelopmentWorkerProfile", b =>
                {
                    b.Property<int>("SkillDevelopmentsSkillID")
                        .HasColumnType("int");

                    b.Property<int>("WorkerProfilesWorkerID")
                        .HasColumnType("int");

                    b.HasKey("SkillDevelopmentsSkillID", "WorkerProfilesWorkerID");

                    b.HasIndex("WorkerProfilesWorkerID");

                    b.ToTable("SkillDevelopmentWorkerProfile");
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

            modelBuilder.Entity("EmployerProfileLogin", b =>
                {
                    b.HasOne("BlueWork.web.Models.EmployerProfile", null)
                        .WithMany()
                        .HasForeignKey("EmployerProfilesEmployerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlueWork.web.Models.Login", null)
                        .WithMany()
                        .HasForeignKey("LoginID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EmployerProfileRegistration", b =>
                {
                    b.HasOne("BlueWork.web.Models.EmployerProfile", null)
                        .WithMany()
                        .HasForeignKey("EmployerProfilesEmployerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlueWork.web.Models.Registration", null)
                        .WithMany()
                        .HasForeignKey("RegistrationID")
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

            modelBuilder.Entity("JobApplicationWorkerProfile", b =>
                {
                    b.HasOne("BlueWork.web.Models.JobApplication", null)
                        .WithMany()
                        .HasForeignKey("JobApplicationsApplicationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlueWork.web.Models.WorkerProfile", null)
                        .WithMany()
                        .HasForeignKey("WorkerProfilesWorkerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LoginRegistration", b =>
                {
                    b.HasOne("BlueWork.web.Models.Login", null)
                        .WithMany()
                        .HasForeignKey("LoginsLoginID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlueWork.web.Models.Registration", null)
                        .WithMany()
                        .HasForeignKey("RegistrationsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LoginWorkerProfile", b =>
                {
                    b.HasOne("BlueWork.web.Models.Login", null)
                        .WithMany()
                        .HasForeignKey("LoginsLoginID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlueWork.web.Models.WorkerProfile", null)
                        .WithMany()
                        .HasForeignKey("WorkerProfilesWorkerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RegistrationWorkerProfile", b =>
                {
                    b.HasOne("BlueWork.web.Models.Registration", null)
                        .WithMany()
                        .HasForeignKey("RegistrationsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlueWork.web.Models.WorkerProfile", null)
                        .WithMany()
                        .HasForeignKey("WorkerProfilesWorkerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SkillDevelopmentWorkerProfile", b =>
                {
                    b.HasOne("BlueWork.web.Models.SkillDevelopment", null)
                        .WithMany()
                        .HasForeignKey("SkillDevelopmentsSkillID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlueWork.web.Models.WorkerProfile", null)
                        .WithMany()
                        .HasForeignKey("WorkerProfilesWorkerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
