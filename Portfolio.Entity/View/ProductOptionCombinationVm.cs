using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Entity.View
{
    public class ProductOptionCombinationVm
    {
        public long OptionId { get; set; }

        public string OptionName { get; set; }

        public string Value { get; set; }

        public int SortIndex { get; set; }
    }
}
