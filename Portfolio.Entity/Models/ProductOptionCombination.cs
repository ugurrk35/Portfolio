using Portfolio.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Entity.Models
{
    public class ProductOptionCombination : EntityBase
    {
        public long ProductId { get; set; }

        public Product Product { get; set; }

        public long OptionId { get; set; }

        public ProductOption Option { get; set; }

        [StringLength(450)]
        public string Value { get; set; }

        public int SortIndex { get; set; }
    }
}
