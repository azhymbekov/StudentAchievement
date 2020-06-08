using Microsoft.AspNetCore.Identity;
using StudentAchievement.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAchievement.Service.Interfaces
{
    public interface IApplicationUserAdministration 
    {
        Task<IdentityResult> CreateAsync(UserModel newUser);

        Task<bool> Edit(UserModel userModel);

        Task<UserForView> UserInfo(Guid userId);

        IQueryable<UserModel> GetListOfUsers();

        Task<UserModel> Get(Guid id);
    }
}
