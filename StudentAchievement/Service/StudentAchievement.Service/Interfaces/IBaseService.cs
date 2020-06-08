using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentAchievement.Service.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        T GetById(Guid id);
        Task<T> GetByIdAsync(Guid id);
        void Remove(Guid id);
        void Remove(T entity);

        T Add(T entity);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        Task<T> UpdateAsync(T entity);
    }
}
