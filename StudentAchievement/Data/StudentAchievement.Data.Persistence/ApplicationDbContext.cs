using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentAchievement.Data.Domain.Entities;
using StudentAchievement.Data.Domain.Entities.Identity;
using StudentAchievement.Data.Persistence.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAchievement.Data.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseUser> CourseStudent { get; set; }

        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new CourseUserMap());
            builder.ApplyConfiguration(new GroupMap());
        }
    }
}
