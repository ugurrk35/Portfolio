using Microsoft.EntityFrameworkCore;
using Portfolio.Data.Abstract;
using Portfolio.Data.Concrete.EntityFramework;
using Portfolio.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Data.Concrete.Repository
{
    public class ProductTemplateProductAttributeRepository : IProductTemplateProductAttributeRepository
    {
        private readonly DbContext dbContext;

        public ProductTemplateProductAttributeRepository(PortfolioDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Remove(ProductTemplateProductAttribute item)
        {
            dbContext.Set<ProductTemplateProductAttribute>().Remove(item);
        }
    }
}
