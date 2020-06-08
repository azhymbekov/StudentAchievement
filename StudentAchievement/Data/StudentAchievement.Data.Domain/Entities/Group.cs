using StudentAchievement.Data.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAchievement.Data.Domain.Entities
{
    public class Group
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> Students { get; set; }
    }
}
