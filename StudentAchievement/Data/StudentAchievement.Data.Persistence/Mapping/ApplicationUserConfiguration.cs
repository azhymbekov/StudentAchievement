using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentAchievement.Data.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAchievement.Data.Persistence.Mapping
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.FirstName).HasColumnType("NVARCHAR(50)").IsRequired();
            builder.Property(x => x.LastName).HasColumnType("NVARCHAR(50)").IsRequired();
            builder.Property(x => x.Patronymic).HasColumnType("NVARCHAR(50)").IsRequired(false);
        }
    }
}
