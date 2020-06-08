using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudentAchievement.Data.Domain.Entities;
using StudentAchievement.Data.Domain.Entities.Identity;
using StudentAchievement.Data.Domain.Interfaces;
using StudentAchievement.Service.Interfaces;
using StudentAchievement.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAchievement.Service.Services
{
    public class ApplicationUserAdministration : BaseService<ApplicationUser>, IApplicationUserAdministration
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IMapper mapper;

        public ApplicationUserAdministration(IUnitOfWork uow, UserManager<ApplicationUser> userManager, IMapper mapper) : base(uow)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }        

        public async Task<IdentityResult> CreateAsync(UserModel newUser)
        {
            var user = _uow.GetRepository<ApplicationUser>().All().Where(x => x.UserName == newUser.Login).FirstOrDefault();
            IdentityResult result = new IdentityResult();
            if (user == null)
            {
                user = mapper.Map<ApplicationUser>(newUser);
                user.Id = Guid.NewGuid();

                    _uow.GetRepository<IdentityUserRole<Guid>>().Add(new IdentityUserRole<Guid>
                    {
                        UserId = user.Id,
                        RoleId = Guid.Parse("62A621A4-79FF-457B-8998-51B152904BF6"),
                    });
                    result = await this.userManager.CreateAsync(user, newUser.Password);

                    return result;                
            }
            throw new InvalidOperationException("Такой пользователь уже есть");
        }

        public IQueryable<UserModel> GetListOfUsers()
        {
            var query = from user in _uow.GetRepository<ApplicationUser>().All()
                        join userRole in this._uow.GetRepository<IdentityUserRole<Guid>>().All()
                        on user.Id equals userRole.UserId
                        join role in this._uow.GetRepository<ApplicationRole>().All()
                        on userRole.RoleId equals role.Id
                        where (role.Name == "Студент")
                        select mapper.Map<UserModel>(user);
                       
            return query;
        }


        public Task<bool> Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public async Task<UserForView> UserInfo(Guid userId)
        {
            var user = await Get(userId);
            var userForView = this.mapper.Map<UserForView>(user);
            var course = from subject in _uow.GetRepository<Course>().All()
                           join ord in _uow.GetRepository<CourseUser>().All() on subject.Id equals ord.CourseId
                           where ord.UserId == userId
                           select subject.Name;           
            userForView.Courses = course;
            return userForView;
        }

        public async Task<UserModel> Get(Guid id)
        {
            var entity = await _uow.GetRepository<ApplicationUser>().FindByIdAsync(id);
            var user = this.mapper.Map<UserModel>(entity);
            return user;
        }

    }
}
