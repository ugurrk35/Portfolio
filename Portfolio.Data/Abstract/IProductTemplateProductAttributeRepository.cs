using Portfolio.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Data.Abstract
{
    public interface IProductTemplateProductAttributeRepository
    {
        void Remove(ProductTemplateProductAttribute item);
    }
}
