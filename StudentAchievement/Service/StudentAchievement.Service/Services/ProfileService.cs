using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudentAchievement.Data.Domain.Entities.Identity;
using StudentAchievement.Data.Domain.Interfaces;
using StudentAchievement.Service.Interfaces;
using StudentAchievement.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentAchievement.Service.Services
{
    public class ProfileService : BaseService<ApplicationUser>, IProfileService
    {
        public readonly IMapper _map;

        private readonly UserManager<ApplicationUser> userManager;

        public ProfileService(IUnitOfWork uow, UserManager<ApplicationUser> userManager, IMapper map) : base(uow)
        {
            _map = map;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword)
        {
            var user = await this._uow.GetRepository<ApplicationUser>().FindByIdAsync(userId);
            var result = await this.userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (result.Succeeded)
            {
                await this._uow.CommitAsync();
            }

            return result;
        }

        public UserModel GetProfile(Guid userId)
        {
            var entity = _uow.GetRepository<ApplicationUser>().FindById(userId);
            return this._map.Map<UserModel>(entity);
        }
    }
}
