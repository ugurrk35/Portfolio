using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Portfolio.Data.Abstract.BaseRepository;
using Portfolio.Data.Concrete.EntityFramework;
using Portfolio.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Data.Concrete.Repository
{
    public class RepositoryWithTypedId<T, TId> : IRepositoryWithTypedId<T, TId> where T : class, IEntityWithTypedId<TId>
    {
        protected DbContext context;
        protected DbSet<T> dbset;
        public RepositoryWithTypedId(PortfolioDbContext context)
        {
            this.context = context;
            this.dbset = this.context.Set<T>();
        }

        public void Add(T entity)
        {
            dbset.Add(entity);
        }

        public void AddRange(IEnumerable<T> entity)
        {
            dbset.AddRange(entity);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return context.Database.BeginTransaction();
        }

        public IQueryable<T> Query()
        {
            return dbset;
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
