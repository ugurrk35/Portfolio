using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Entity.View
{
    public class ProductAttributeFormVm
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The Attribute Group field is required.")]
        public long GroupId { get; set; }
    }
}
