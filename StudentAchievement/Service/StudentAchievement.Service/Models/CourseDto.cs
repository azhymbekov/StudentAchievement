using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentAchievement.Service.Models
{
    public class CourseDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Введите название предмета")]
        public string Name { get; set; }

        public int Grade { get; set; }

        public string TeacherName { get; set; }

        public Guid TeacherId { get; set; }
    }
}
