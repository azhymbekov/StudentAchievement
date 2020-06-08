using StudentAchievement.Data.Domain.Interfaces;
using StudentAchievement.Data.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAchievement.Data.Persistence
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly List<object> _repositories = new List<object>();

        public UnitOfWork(ApplicationDbContext _context )
        {
            this._context = _context;
        }

      
        public int Commit()
        {
            return _context.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            var repo = (IRepository<T>)_repositories.SingleOrDefault(r => r is IRepository<T>);
            if (repo == null)
            {
                _repositories.Add(repo = new EntityRepository<T>(_context));
            }
            return repo;
        }

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool isDisposing)
        {
            if (!_disposed)
            {
                if (isDisposing)
                {
                    if (_context != null)
                    {
                        _context.Dispose();
                    }
                }
            }

            _disposed = true;
        }
    }
}
