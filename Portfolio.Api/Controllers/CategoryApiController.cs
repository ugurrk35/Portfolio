using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data.Abstract.BaseRepository;
using Portfolio.Entity.Models;
using Portfolio.Entity.View;
using Portfolio.Services.Abstract;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using Portfolio.Api.Extension;

namespace Portfolio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<ProductCategory> _productCategoryRepository;
        private readonly ICategoryService _categoryService;
        private readonly IMediaService _mediaService;
        public CategoryApiController(IRepository<Category> categoryRepository, IRepository<ProductCategory> productCategoryRepository, ICategoryService categoryService, IMediaService mediaService)
        {
            _categoryRepository = categoryRepository;
            _productCategoryRepository = productCategoryRepository;
            _categoryService = categoryService;
            _mediaService = mediaService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var category = await _categoryRepository.Query().Include(x => x.ThumbnailImage).FirstOrDefaultAsync(x => x.Id == id);
            var model = new CategoryForm
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug,
                MetaTitle = category.MetaTitle,
                MetaKeywords = category.MetaKeywords,
                MetaDescription = category.MetaDescription,
                DisplayOrder = category.DisplayOrder,
                Description = category.Description,
           
                IncludeInMenu = category.IncludeInMenu,
                IsPublished = category.IsPublished,
                ThumbnailImageUrl = _mediaService.GetThumbnailUrl(category.ThumbnailImage),
            };

            return Ok(model);
        }
        [HttpPost]
       
        public async Task<IActionResult> Post(CategoryForm model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = model.Name,
                    Slug = model.Slug,
                    MetaTitle = model.MetaTitle,
                    MetaKeywords = model.MetaKeywords,
                    MetaDescription = model.MetaDescription,
                    DisplayOrder = model.DisplayOrder,
                    Description = model.Description,
                
                    IncludeInMenu = model.IncludeInMenu,
                    IsPublished = model.IsPublished
                };

                await SaveCategoryImage(category, model);
                await _categoryService.Create(category);
                return CreatedAtAction(nameof(Get), new { id = category.Id }, null);
            }

            return BadRequest(ModelState);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var gridData = await _categoryService.GetAll();
            return Ok(gridData);
        }

        [HttpPost("{id}/products")]
        public IActionResult GetProducts(long id, [FromBody] SmartTableParam param)
        {
            var query = _productCategoryRepository.Query().Include(x => x.Product)
                .Where(x => x.CategoryId == id && !x.Product.IsDeleted && x.Product.IsVisibleIndividually);

            if (param.Search.PredicateObject != null)
            {
                dynamic search = param.Search.PredicateObject;
                if (search.Name != null)
                {
                    string name = search.Name;
                    query = query.Where(x => x.Product.Name.Contains(name));
                }

                if (search.IsPublished != null)
                {
                    bool isPublished = search.IsPublished;
                    query = query.Where(x => x.Product.IsPublished == isPublished);
                }
            }

            var gridData = query.ToSmartTableResult(
                param,
                x => new
                {
                    Id = x.Id,
                    ProductName = x.Product.Name,
                    IsFeaturedProduct = x.IsFeaturedProduct,
                    DisplayOrder = x.DisplayOrder,
                    IsProductPublished = x.Product.IsPublished
                });

            return Ok(gridData);
        }
        private async Task SaveCategoryImage(Category category, CategoryForm model)
        {
            if (model.ThumbnailImage != null)
            {
                var fileName = await SaveFile(model.ThumbnailImage);
                if (category.ThumbnailImage != null)
                {
                    category.ThumbnailImage.FileName = fileName;
                }
                else
                {
                    category.ThumbnailImage = new Media { FileName = fileName };
                }
            }
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Value.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _mediaService.SaveMediaAsync(file.OpenReadStream(), fileName, file.ContentType);
            return fileName;
        }
    }
}
