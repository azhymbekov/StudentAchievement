using Microsoft.AspNetCore.Identity;
using StudentAchievement.Data.Domain.Entities.Identity;
using StudentAchievement.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentAchievement.Service.Interfaces
{
    public interface IProfileService : IBaseService<ApplicationUser>
    {
        UserModel GetProfile(Guid userId);

        Task<IdentityResult> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword);
    }
}
