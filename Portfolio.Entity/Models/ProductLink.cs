using Portfolio.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Entity.Models
{
    public class ProductLink : EntityBase
    {
        public long ProductId { get; set; }

        public Product Product { get; set; }

        public long LinkedProductId { get; set; }

        public Product LinkedProduct { get; set; }

        public ProductLinkType LinkType { get; set; }
    }
}
