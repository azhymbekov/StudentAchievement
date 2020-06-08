using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAchievement.Service.Models
{
    public class GroupDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public  List<UserModel> Students { get; set; }
    }
}
