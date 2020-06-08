using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAchievement.Service.Models
{
    public class StudentCourses
    {
        public StudentsForDisplay Student { get; set; }

        public Guid[] CurrentCourseIds { get; set; }

        public Dictionary<Guid, string> Courses { get; set; }
    }
}
