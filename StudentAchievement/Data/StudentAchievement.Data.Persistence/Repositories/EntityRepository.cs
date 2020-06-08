using Microsoft.EntityFrameworkCore;
using StudentAchievement.Data.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentAchievement.Data.Persistence.Repositories
{
    public class EntityRepository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> DbSet { get; set; }

        protected ApplicationDbContext Context { get; set; }

        public EntityRepository(ApplicationDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            this.DbSet = this.Context.Set<T>();
        }

        public virtual IQueryable<T> All()
        {
            return DbSet;
        }

        public virtual IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = All();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public virtual T FindById(object id)
        {
            return DbSet.Find(id);
        }

        public virtual async Task<T> FindByIdAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual IQueryable<T> Where(params Expression<Func<T, bool>>[] predicates)
        {
            IQueryable<T> query = All();
            foreach (var predicate in predicates)
            {
                query = query.Where(predicate);
            }
            return query;
        }

        public virtual async Task<T> FirstOrDefaultAsync()
        {
            return await All().FirstOrDefaultAsync();
        }

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicates)
        {
            return await All().FirstOrDefaultAsync(predicates);
        }

        public void Add(T entity) => this.DbSet.Add(entity);

        public virtual void Update(T entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual void Remove(T entity) => this.DbSet.Remove(entity);

        public virtual void Remove(object id)
        {
            var entity = FindById(id);
            if (entity == null) return; // not found; assume already deleted.
            Remove(entity);
        }

        public void Remove(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

    }
}
