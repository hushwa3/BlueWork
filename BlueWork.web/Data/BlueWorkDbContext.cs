using BlueWork.web.BlueWorkAuth;
using BlueWork.web.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlueWork.web.Data
{
    public class BlueWorkDbContext : DbContext
    {
        public BlueWorkDbContext(DbContextOptions<BlueWorkDbContext> options) : base(options)
        {
        }

        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<EmployerProfile> EmployerProfiles { get; set; }
        public DbSet<WorkerProfile> WorkerProfiles { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobListing> JobListings { get; set; }
        public DbSet<SkillDevelopment> SkillDevelopments { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply the Index attribute on Email for uniqueness
            modelBuilder.Entity<UserAccount>()
                .HasIndex(u => u.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

    }
}