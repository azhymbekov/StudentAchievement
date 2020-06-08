using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudentAchievement.Data.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace StudentAchievement.Data.Persistence
{
    public class ApplicationRoleStore : RoleStore<
       ApplicationRole,
       ApplicationDbContext,
       Guid,
       IdentityUserRole<Guid>,
       IdentityRoleClaim<Guid>>
    {
        public ApplicationRoleStore(ApplicationDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }

        protected override IdentityRoleClaim<Guid> CreateRoleClaim(ApplicationRole role, Claim claim) =>
            new IdentityRoleClaim<Guid>
            {
                RoleId = role.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
            };
    }
}
