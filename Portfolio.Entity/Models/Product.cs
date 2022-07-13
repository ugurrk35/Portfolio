using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Entity.Models
{
    public class Product: Content
    {
        [StringLength(450)]
        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string Specification { get; set; }

        public bool HasOptions { get; set; }

        public bool IsVisibleIndividually { get; set; }

        public bool IsFeatured { get; set; }

        [StringLength(450)]
        public string Sku { get; set; }

        [StringLength(450)]
        public string Gtin { get; set; }

        public Media ThumbnailImage { get; set; }

        public IList<ProductMedia> Medias { get; protected set; } = new List<ProductMedia>();

        public IList<ProductLink> ProductLinks { get; protected set; } = new List<ProductLink>();

        public IList<ProductLink> LinkedProductLinks { get; protected set; } = new List<ProductLink>();

        public IList<ProductAttributeValue> AttributeValues { get; protected set; } = new List<ProductAttributeValue>();

        public IList<ProductOptionValue> OptionValues { get; protected set; } = new List<ProductOptionValue>();

        public IList<ProductCategory> Categories { get; protected set; } = new List<ProductCategory>();



        public void AddCategory(ProductCategory category)
        {
            category.Product = this;
            Categories.Add(category);
        }

        public void AddMedia(ProductMedia media)
        {
            media.Product = this;
            Medias.Add(media);
        }

        public void AddAttributeValue(ProductAttributeValue attributeValue)
        {
            attributeValue.Product = this;
            AttributeValues.Add(attributeValue);
        }

        public void AddOptionValue(ProductOptionValue optionValue)
        {
            optionValue.Product = this;
            OptionValues.Add(optionValue);
        }

        public void AddProductLinks(ProductLink productLink)
        {
            productLink.Product = this;
            ProductLinks.Add(productLink);
        }

        public IList<ProductOptionCombination> OptionCombinations { get; protected set; } = new List<ProductOptionCombination>();

        public void AddOptionCombination(ProductOptionCombination combination)
        {
            combination.Product = this;
            OptionCombinations.Add(combination);
        }

        public Product Clone()
        {
            var product = new Product();
            product.Name = Name;
            product.MetaTitle = MetaTitle;
            product.MetaKeywords = MetaKeywords;
            product.MetaDescription = MetaDescription;
            product.ShortDescription = ShortDescription;
            product.Description = Description;
            product.Specification = Specification;
            product.IsPublished = IsPublished;
            product.HasOptions = HasOptions;
            product.IsVisibleIndividually = IsVisibleIndividually;
            product.IsFeatured = IsFeatured;       
            product.Sku = Sku;
            product.Gtin = Gtin;
            product.Slug = Slug;

            foreach (var attribute in AttributeValues)
            {
                product.AddAttributeValue(new ProductAttributeValue
                {
                    AttributeId = attribute.AttributeId,
                    Value = attribute.Value
                });
            }

            foreach (var category in Categories)
            {
                product.AddCategory(new ProductCategory
                {
                    CategoryId = category.CategoryId
                });
            }

            return product;
        }
    }
}