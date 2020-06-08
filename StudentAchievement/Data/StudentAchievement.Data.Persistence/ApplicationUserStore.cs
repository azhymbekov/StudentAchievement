using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudentAchievement.Data.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace StudentAchievement.Data.Persistence
{
    public class ApplicationUserStore : UserStore<
        ApplicationUser,
        ApplicationRole,
        ApplicationDbContext,
        Guid,
        IdentityUserClaim<Guid>,
        IdentityUserRole<Guid>,
        IdentityUserLogin<Guid>,
        IdentityUserToken<Guid>,
        IdentityRoleClaim<Guid>>
    {
        public ApplicationUserStore(ApplicationDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }

        protected override IdentityUserRole<Guid> CreateUserRole(ApplicationUser user, ApplicationRole role)
        {
            return new IdentityUserRole<Guid> { RoleId = role.Id, UserId = user.Id };
        }

        protected override IdentityUserClaim<Guid> CreateUserClaim(ApplicationUser user, Claim claim)
        {
            var identityUserClaim = new IdentityUserClaim<Guid> { UserId = user.Id };
            identityUserClaim.InitializeFromClaim(claim);
            return identityUserClaim;
        }

        protected override IdentityUserLogin<Guid> CreateUserLogin(ApplicationUser user, UserLoginInfo login) =>
            new IdentityUserLogin<Guid>
            {
                UserId = user.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName,
            };

        protected override IdentityUserToken<Guid> CreateUserToken(
            ApplicationUser user,
            string loginProvider,
            string name,
            string value)
        {
            var token = new IdentityUserToken<Guid>
            {
                UserId = user.Id,
                LoginProvider = loginProvider,
                Name = name,
                Value = value,
            };
            return token;
        }
    }
}
