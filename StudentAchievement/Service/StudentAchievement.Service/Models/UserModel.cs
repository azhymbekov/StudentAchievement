using StudentAchievement.Data.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentAchievement.Service.Models
{
    public class UserModel : IEquatable<UserModel>
    {
        public Guid Id { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        public string Role { get; set; }

        [Display(Name = "Роль")]
        [Required]
        public Guid? RoleId { get; set; }
       

        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        [Required(ErrorMessage = "Введите Email")]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "GeneratePassword")]
        public string Password { get; set; }

        public bool Equals(UserModel other)
        {
            //Check whether the compared object is null. 
            if (object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (object.ReferenceEquals(this, other)) return true;

            //Check whether properties are equal. 
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            //Get hash code for the Id field if it is not null. 
            return Id == null ? 0 : Id.GetHashCode();
        }


    }
}
