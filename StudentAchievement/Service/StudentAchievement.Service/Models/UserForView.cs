using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentAchievement.Service.Models
{
    public class UserForView
    {
        public Guid Id { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Отчетсво")]
        public string Patronymic { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public IEnumerable<string> Courses { get; set; }
    }
}
