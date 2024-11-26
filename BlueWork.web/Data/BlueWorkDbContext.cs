﻿using BlueWork.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlueWork.web.Data
{
    public class BlueWorkDbContext(DbContextOptions<BlueWorkDbContext> options) : DbContext(options)
    {
        public DbSet<EmployerProfile> EmployerProfiles { get; set; }
        public DbSet<WorkerProfile> WorkerProfiles { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobListing> JobListings { get; set; }
        public DbSet<SkillDevelopment> SkillDevelopments { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            // Example of defining composite keys
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });
        }

    }
}
