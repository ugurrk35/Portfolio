using Microsoft.EntityFrameworkCore;
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
    public class Repository<T> : RepositoryWithTypedId<T, long>, IRepository<T>
        where T : class, IEntityWithTypedId<long>
    {
      
        public Repository(PortfolioDbContext context) : base(context)
        {
        }
    }
}
