using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Data.Concrete.Mapping
{
    internal class ProductTemplateProductAttributeMap : IEntityTypeConfiguration<ProductTemplateProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductTemplateProductAttribute> builder)
        {

            builder.HasKey(t => new { t.ProductTemplateId, t.ProductAttributeId });

            builder.HasOne(pt => pt.ProductTemplate)
                .WithMany(p => p.ProductAttributes)
                .HasForeignKey(pt => pt.ProductTemplateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pt => pt.ProductAttribute)
                .WithMany(t => t.ProductTemplates)
                .HasForeignKey(pt => pt.ProductAttributeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
