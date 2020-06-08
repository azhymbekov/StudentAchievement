using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentAchievement.Data.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAchievement.Data.Persistence.Mapping
{
    public class CourseUserMap : IEntityTypeConfiguration<CourseUser>
    {
        public void Configure(EntityTypeBuilder<CourseUser> builder)
        {
            builder.HasKey(x => new { x.CourseId, x.UserId });
            builder.HasOne(x => x.User).WithMany(x => x.Courses).HasForeignKey(x => x.UserId).IsRequired();
            builder.HasOne(x => x.Course).WithMany(x => x.Users).HasForeignKey(x => x.CourseId).IsRequired();
  
        }
    }
}
