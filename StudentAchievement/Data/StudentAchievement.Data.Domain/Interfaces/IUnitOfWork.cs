using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentAchievement.Data.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;
        Task<int> CommitAsync();
        int Commit();
    }
}
