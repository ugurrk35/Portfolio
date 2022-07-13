using Portfolio.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.Abstract
{
    public interface IProductService
    {
        void Create(Product product);

        void Update(Product product);

        Task Delete(Product product);
    }
}
