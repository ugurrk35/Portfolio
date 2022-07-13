using Portfolio.Entity.Models;
using Portfolio.Entity.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IList<CategoryListItem>> GetAll();

        Task Create(Category category);

        Task Update(Category category);

        Task Delete(Category category);
    }
}
