using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Portfolio.Data.Abstract.BaseRepository;
using Portfolio.Entity.Models;
using Portfolio.Entity.View;
using Portfolio.Services.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace Portfolio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IMediaService _mediaService;
        private readonly IRepository<ProductAttributeValue> _productAttributeValueRepository;
        private readonly IRepository<ProductCategory> _productCategoryRepository;
        private readonly IRepository<ProductLink> _productLinkRepository;
        private readonly IRepository<ProductOptionValue> _productOptionValueRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IProductService _productService;
        private readonly IRepository<ProductMedia> _productMediaRepository;

        public ProductApiController(
            IRepository<Product> productRepository,
            IMediaService mediaService,
            IProductService productService,
            IRepository<ProductLink> productLinkRepository,
            IRepository<ProductCategory> productCategoryRepository,
            IRepository<ProductOptionValue> productOptionValueRepository,
            IRepository<ProductAttributeValue> productAttributeValueRepository,
            IRepository<ProductMedia> productMediaRepository)
 
        {
            _productRepository = productRepository;
            _mediaService = mediaService;
            _productService = productService;
            _productLinkRepository = productLinkRepository;
            _productCategoryRepository = productCategoryRepository;
            _productOptionValueRepository = productOptionValueRepository;
            _productAttributeValueRepository = productAttributeValueRepository;
            _productMediaRepository = productMediaRepository;
  
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var product = _productRepository.Query()
                .Include(x => x.ThumbnailImage)
                .Include(x => x.Medias).ThenInclude(m => m.Media)
                .Include(x => x.ProductLinks).ThenInclude(p => p.LinkedProduct).ThenInclude(m => m.ThumbnailImage)
                .Include(x => x.OptionValues).ThenInclude(o => o.Option)
                .Include(x => x.AttributeValues).ThenInclude(a => a.Attribute).ThenInclude(g => g.Group)
                .Include(x => x.Categories)
                .FirstOrDefault(x => x.Id == id);

        

            var productVm = new ProductVm
            {
                Id = product.Id,
                Name = product.Name,
                Slug = product.Slug,
                MetaTitle = product.MetaTitle,
                MetaKeywords = product.MetaKeywords,
                MetaDescription = product.MetaDescription,
                Sku = product.Sku,
                Gtin = product.Gtin,
                ShortDescription = product.ShortDescription,
                Description = product.Description,
                Specification = product.Specification, 
                IsFeatured = product.IsFeatured,
                IsPublished = product.IsPublished,     
                CategoryIds = product.Categories.Select(x => x.CategoryId).ToList(),
                ThumbnailImageUrl = _mediaService.GetThumbnailUrl(product.ThumbnailImage),  
               
            };

            foreach (var productMedia in product.Medias.Where(x => x.Media.MediaType == MediaType.Image))
            {
                productVm.ProductImages.Add(new ProductMediaVm
                {
                    Id = productMedia.Id,
                    MediaUrl = _mediaService.GetThumbnailUrl(productMedia.Media)
                });
            }

            foreach (var productMedia in product.Medias.Where(x => x.Media.MediaType == MediaType.File))
            {
                productVm.ProductDocuments.Add(new ProductMediaVm
                {
                    Id = productMedia.Id,
                    Caption = productMedia.Media.Caption,
                    MediaUrl = _mediaService.GetMediaUrl(productMedia.Media)
                });
            }


            productVm.Options = product.OptionValues.OrderBy(x => x.SortIndex).Select(x =>
                new ProductOptionVm
                {
                    Id = x.OptionId,
                    Name = x.Option.Name,
                    DisplayType = x.DisplayType,
                    Values = JsonConvert.DeserializeObject<IList<ProductOptionValueVm>>(x.Value)
                }).ToList();

            foreach (var variation in product.ProductLinks.Where(x => x.LinkType == ProductLinkType.Super).Select(x => x.LinkedProduct).Where(x => !x.IsDeleted).OrderBy(x => x.Id))
            {
                productVm.Variations.Add(new ProductVariationVm
                {
                    Id = variation.Id,
                    Name = variation.Name,
                    Sku = variation.Sku,
                    Gtin = variation.Gtin,        
                    ThumbnailImageUrl = _mediaService.GetMediaUrl(variation.ThumbnailImage),
                    ImageUrls = GetProductImageUrls(variation.Id).ToList(),
                    OptionCombinations = variation.OptionCombinations.Select(x => new ProductOptionCombinationVm
                    {
                        OptionId = x.OptionId,
                        OptionName = x.Option.Name,
                        Value = x.Value,
                        SortIndex = x.SortIndex
                    }).OrderBy(x => x.SortIndex).ToList()
                });
            }

            foreach (var relatedProduct in product.ProductLinks.Where(x => x.LinkType == ProductLinkType.Related).Select(x => x.LinkedProduct).Where(x => !x.IsDeleted).OrderBy(x => x.Id))
            {
                productVm.RelatedProducts.Add(new ProductLinkVm
                {
                    Id = relatedProduct.Id,
                    Name = relatedProduct.Name,
                    IsPublished = relatedProduct.IsPublished
                });
            }

           

            productVm.Attributes = product.AttributeValues.Select(x => new ProductAttributeVm
            {
                AttributeValueId = x.Id,
                Id = x.AttributeId,
                Name = x.Attribute.Name,
                GroupName = x.Attribute.Group.Name,
                Value = x.Value
            }).ToList();

            return Ok(productVm);
        }
        private IEnumerable<string> GetProductImageUrls(long productId)
        {
            var imageUrls = _productMediaRepository.Query()
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.Id)
                .Select(x => x.Media)
                .ToList()
                .Select(x => _mediaService.GetMediaUrl(x));

            return imageUrls;
        }
        [HttpPost]
        public async Task<IActionResult> Post(ProductForm model)
        {
            MapUploadedFile(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            var product = new Product
            {
                Name = model.Product.Name,
                Slug = model.Product.Slug,
                MetaTitle = model.Product.MetaTitle,
                MetaKeywords = model.Product.MetaKeywords,
                MetaDescription = model.Product.MetaDescription,
                Sku = model.Product.Sku,
                Gtin = model.Product.Gtin,
                ShortDescription = model.Product.ShortDescription,
                Description = model.Product.Description,
                Specification = model.Product.Specification,
             
                IsPublished = model.Product.IsPublished,
                IsFeatured = model.Product.IsFeatured,
               
                HasOptions = model.Product.Variations.Any() ? true : false,
                IsVisibleIndividually = true,
              
            };

           

            var optionIndex = 0;
            foreach (var option in model.Product.Options)
            {
                product.AddOptionValue(new ProductOptionValue
                {
                    OptionId = option.Id,
                    DisplayType = option.DisplayType,
                    Value = JsonConvert.SerializeObject(option.Values),
                    SortIndex = optionIndex
                });

                optionIndex++;
            }

            foreach (var attribute in model.Product.Attributes)
            {
                var attributeValue = new ProductAttributeValue
                {
                    AttributeId = attribute.Id,
                    Value = attribute.Value
                };

                product.AddAttributeValue(attributeValue);
            }

            foreach (var categoryId in model.Product.CategoryIds)
            {
                var productCategory = new ProductCategory
                {
                    CategoryId = categoryId
                };
                product.AddCategory(productCategory);
            }

            await SaveProductMedias(model, product);

            await MapProductVariationVmToProduct(model, product);
            MapProductLinkVmToProduct(model, product);

            

            _productService.Create(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, null);
        }

        private void MapUploadedFile(ProductForm model)
        {
            // Currently model binder cannot map the collection of file productImages[0], productImages[1]
            foreach (var file in Request.Form.Files)
            {
                if (file.Name.Contains("productImages"))
                {
                    model.ProductImages.Add(file);
                }
                else if (file.Name.Contains("productDocuments"))
                {
                    model.ProductDocuments.Add(file);
                }
                else if (file.Name.Contains("product[variations]"))
                {
                    var key = file.Name.Replace("product", "");
                    var keyParts = key.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                    var variantIndex = int.Parse(keyParts[1]);
                    if (key.Contains("newImages"))
                    {
                        model.Product.Variations[variantIndex].NewImages.Add(file);
                    }
                    else
                    {
                        model.Product.Variations[variantIndex].ThumbnailImage = file;
                    }
                }
            }
        }
        private async Task SaveProductMedias(ProductForm model, Product product)
        {
            if (model.ThumbnailImage != null)
            {
                var fileName = await SaveFile(model.ThumbnailImage);
                if (product.ThumbnailImage != null)
                {
                    product.ThumbnailImage.FileName = fileName;
                }
                else
                {
                    product.ThumbnailImage = new Media { FileName = fileName };
                }
            }

            foreach (var file in model.ProductImages)
            {
                var fileName = await SaveFile(file);
                var productMedia = new ProductMedia
                {
                    Product = product,
                    Media = new Media { FileName = fileName, MediaType = MediaType.Image }
                };
                product.AddMedia(productMedia);
            }

            foreach (var file in model.ProductDocuments)
            {
                var fileName = await SaveFile(file);
                var productMedia = new ProductMedia
                {
                    Product = product,
                    Media = new Media { FileName = fileName, MediaType = MediaType.File, Caption = file.FileName }
                };
                product.AddMedia(productMedia);
            }
        }
        private async Task MapProductVariationVmToProduct(ProductForm model, Product product)
        {
            foreach (var variationVm in model.Product.Variations)
            {
                var productLink = new ProductLink
                {
                    LinkType = ProductLinkType.Super,
                    Product = product,
                    LinkedProduct = product.Clone()
                };

           
                productLink.LinkedProduct.Name = variationVm.Name;          
                productLink.LinkedProduct.Sku = variationVm.Sku;
                productLink.LinkedProduct.Gtin = variationVm.Gtin;
             
                productLink.LinkedProduct.HasOptions = false;
                productLink.LinkedProduct.IsVisibleIndividually = false;

                if (product.ThumbnailImage != null)
                {
                    productLink.LinkedProduct.ThumbnailImage = new Media { FileName = product.ThumbnailImage.FileName };
                }

                await MapProductVariantImageFromVm(variationVm, productLink.LinkedProduct);

                foreach (var combinationVm in variationVm.OptionCombinations)
                {
                    productLink.LinkedProduct.AddOptionCombination(new ProductOptionCombination
                    {
                        OptionId = combinationVm.OptionId,
                        Value = combinationVm.Value,
                        SortIndex = combinationVm.SortIndex
                    });
                }

              

                product.AddProductLinks(productLink);
            }
        }
        private async Task MapProductVariantImageFromVm(ProductVariationVm variationVm, Product product)
        {
            if (variationVm.ThumbnailImage != null)
            {
                var thumbnailImageFileName = await SaveFile(variationVm.ThumbnailImage);
                if (product.ThumbnailImage != null)
                {
                    product.ThumbnailImage.FileName = thumbnailImageFileName;
                }
                else
                {
                    product.ThumbnailImage = new Media { FileName = thumbnailImageFileName };
                }
            }

            foreach (var image in variationVm.NewImages)
            {
                var fileName = await SaveFile(image);
                var productMedia = new ProductMedia
                {
                    Product = product,
                    Media = new Media { FileName = fileName, MediaType = MediaType.Image }
                };

                product.AddMedia(productMedia);
            }
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _mediaService.SaveMediaAsync(file.OpenReadStream(), fileName, file.ContentType);
            return fileName;
        }
        private static void MapProductLinkVmToProduct(ProductForm model, Product product)
        {
            foreach (var relatedProductVm in model.Product.RelatedProducts)
            {
                var productLink = new ProductLink
                {
                    LinkType = ProductLinkType.Related,
                    Product = product,
                    LinkedProductId = relatedProductVm.Id
                };

                product.AddProductLinks(productLink);
            }

        }
    }
}
