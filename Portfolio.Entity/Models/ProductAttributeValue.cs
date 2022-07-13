using Portfolio.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Entity.Models
{
   public class ProductAttributeValue : EntityBase
    {
        public long AttributeId { get; set; }

        public ProductAttribute Attribute { get; set; }

        public long ProductId { get; set; }

        public Product Product { get; set; }

        public string Value { get; set; }
    }
}
