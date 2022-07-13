using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Entity.View
{
    public class CategoryListItem
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public bool IncludeInMenu { get; set; }

        public bool IsPublished { get; set; }

       
    }
}
