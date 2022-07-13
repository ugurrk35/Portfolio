using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Entity.View
{
    public class ProductOptionVm
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string DisplayType { get; set; }

        public IList<ProductOptionValueVm> Values { get; set; } = new List<ProductOptionValueVm>();
    }
}
