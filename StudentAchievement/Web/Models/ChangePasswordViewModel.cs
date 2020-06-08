using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAchievement.Models
{
    public class ChangePasswordViewModel
    {
        public string UserName { get; set; }

        public bool IsPresistent { get; set; }

        [Display(Name = "Старый пароль")]
        [Required(ErrorMessage = "Введите старый пароль")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "Новый пароль")]
        [Required(ErrorMessage = "Введите новый пароль")]
        [StringLength(20, ErrorMessage = "PasswordLengthError", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Подтвердите пароль")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Подтвердите пароль")]
        public string ConfirmPassword { get; set; }
    }
}
