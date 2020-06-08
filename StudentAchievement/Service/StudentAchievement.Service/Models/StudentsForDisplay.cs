using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAchievement.Service.Models
{
    public class StudentsForDisplay
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid[] Courses { get; set; }
    }
}
