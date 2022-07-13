using Microsoft.EntityFrameworkCore;
using Portfolio.Data.Concrete.Mapping;
using Portfolio.Entity.Models;
using Portfolio.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Data.Concrete.EntityFramework
{
    public class PortfolioDbContext: DbContext
    {
        //public PortfolioDbContext(DbContextOptions options) : base(options)
        //{
        //}
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
      : base(options)
        { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server=DESKTOP-8I58UG8\\SQLEXPRESS; database = Portfoliod; Trusted_Connection= true");

        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductLinkMap());
            modelBuilder.ApplyConfiguration(new ProductTemplateProductAttributeMap());

            modelBuilder.Entity<PEntity>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.EntityId);
            });
        }





        public DbSet<Category> Categories { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductAttributeGroup> ProductAttributeGroups { get; set; }
        public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductLink> ProductLinks { get; set; }
        public DbSet<ProductMedia> ProductMedias { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<ProductOptionCombination> ProductOptionCombinations { get; set; }
        public DbSet<ProductOptionValue> ProductOptionValues { get; set; }
        public DbSet<ProductTemplate> ProductTemplates { get; set; }
        public DbSet<ProductTemplateProductAttribute> ProductTemplateProductAttributes { get; set; }
        
    }
}
