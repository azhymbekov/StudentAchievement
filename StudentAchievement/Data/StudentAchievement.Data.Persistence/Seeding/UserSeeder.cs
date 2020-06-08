using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentAchievement.Data.Domain.Entities.Identity;
using StudentAchievement.Data.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace StudentAchievement.Data.Persistence.Seeding
{
    public class UserSeeder
    {
        private const string AdminLogin = "admin";
        private const string AdminEmail = "test@gmail.com";
        private const string AdminPhone = "+996555121232";
        private const string Password = "1qaz@WSX";

        public async Task SeedAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            await SeedUserAsync(roleManager ,userManager, context);  
        }

      
        


        private static async Task SeedUserAsync(
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            var role = await roleManager.FindByNameAsync(UserRoles.Admin);          

            if(role == null)
            {
                role = new ApplicationRole
                {
                    Id = Guid.NewGuid()
                };
                await roleManager.CreateAsync(new ApplicationRole { Id = role.Id, Name = UserRoles.Admin });
            }
            var studentRole = await roleManager.FindByNameAsync(UserRoles.Student);
            if ( studentRole == null)
            {
                await roleManager.CreateAsync(new ApplicationRole { Id = Guid.NewGuid(), Name = UserRoles.Student });
            }

            var teacherRole = await roleManager.FindByNameAsync(UserRoles.Teacher);
            if (teacherRole == null)
            {
                await roleManager.CreateAsync(new ApplicationRole { Id = Guid.NewGuid(), Name = UserRoles.Teacher });
            }

            var user = await userManager.FindByNameAsync(AdminLogin);
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = AdminLogin,
                    Email = AdminEmail,
                    EmailConfirmed = true,
                    PhoneNumber = AdminPhone,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    FirstName = "Админ",
                    LastName = "Главный",
                    Patronymic = "Системы",
                };
                var result = await userManager.CreateAsync(user, Password);
                if (!result.Succeeded)
                {
                    return;
                }
                else
                {
                    context.UserRoles.Add(new IdentityUserRole<Guid>
                    {
                        UserId = user.Id,
                        RoleId = role.Id,
                    });
                    
                }
            }

            await context.SaveChangesAsync();
        }
        
    }
}
