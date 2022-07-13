using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Entity.View
{
    public class ProductAttributeVm
    {
        public long Id { get; set; }

        public long AttributeValueId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string GroupName { get; set; }
    }
}
