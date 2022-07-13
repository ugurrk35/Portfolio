using Microsoft.EntityFrameworkCore;
using Portfolio.Data.Abstract.BaseRepository;
using Portfolio.Entity.Models;
using Portfolio.Entity.View;
using Portfolio.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private const string CategoryEntityTypeId = "Category";
        private readonly IRepository<Category> _categoryRepository;
  
        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
           
        }

        public async Task Create(Category category)
        {
            using (var transaction = _categoryRepository.BeginTransaction())
            {
               
                _categoryRepository.Add(category);
                await _categoryRepository.SaveChangesAsync();

                
                await _categoryRepository.SaveChangesAsync();

                transaction.Commit();
            }
        }

        public async Task Delete(Category category)
        {
          
            category.IsDeleted = true;
            _categoryRepository.SaveChanges();
        }

        public async Task<IList<CategoryListItem>> GetAll()
        {
            var categories = await _categoryRepository.Query().Where(x => !x.IsDeleted).ToListAsync();
            var categoriesList = new List<CategoryListItem>();
            foreach (var category in categories)
            {
                var categoryListItem = new CategoryListItem
                {
                    Id = category.Id,
                    IsPublished = category.IsPublished,
                    IncludeInMenu = category.IncludeInMenu,
                    Name = category.Name,
                    DisplayOrder = category.DisplayOrder,
                   
                };

                var parentCategory = category.Parent;
                while (parentCategory != null)
                {
                    categoryListItem.Name = $"{parentCategory.Name} >> {categoryListItem.Name}";
                    parentCategory = parentCategory.Parent;
                }

                categoriesList.Add(categoryListItem);
            }

            return categoriesList.OrderBy(x => x.Name).ToList();
        }

        public async Task Update(Category category)
        {
           
            await _categoryRepository.SaveChangesAsync();
        }
    }
}
