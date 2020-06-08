using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentAchievement.Data.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAchievement.Data.Persistence.Mapping
{
    public class GroupMap : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasMany(x => x.Students).WithOne(x => x.Group).IsRequired(false);
        }
    }
}
