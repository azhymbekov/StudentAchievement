using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentAchievement.Data.Persistence.Seeding
{
    public interface ISeeder
    {
        Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider);
    }
}
