using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAchievement.Data.Domain.Entities
{
    public class Course 
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Grade { get; set; }

        public virtual ICollection<CourseUser> Users { get; set; }

    }
}
