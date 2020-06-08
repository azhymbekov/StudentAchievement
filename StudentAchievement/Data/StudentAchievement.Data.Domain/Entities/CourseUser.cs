using StudentAchievement.Data.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAchievement.Data.Domain.Entities
{
    public class CourseUser
    {
        public Guid CourseId { get; set; }

        public Course Course { get; set; }

        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
