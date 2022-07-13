using Portfolio.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Entity.Models
{
    public class ProductCategory : EntityBase
    {
        public bool IsFeaturedProduct { get; set; }

        public int DisplayOrder { get; set; }

        public long CategoryId { get; set; }

        public long ProductId { get; set; }

        public Category Category { get; set; }

        public Product Product { get; set; }
    }
}
