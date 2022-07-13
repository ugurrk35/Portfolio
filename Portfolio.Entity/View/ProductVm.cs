using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Entity.View
{
    public class ProductVm
    {
        public ProductVm()
        {
            IsPublished = true;
            IsAllowToOrder = true;
         
        }

        public long Id { get; set; }


        public bool IsAllowToOrder { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public string Slug { get; set; }

        public string MetaTitle { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string Sku { get; set; }

        public string Gtin { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string Specification { get; set; }

        public bool IsPublished { get; set; }

        public bool IsFeatured { get; set; }


        public IList<long> CategoryIds { get; set; } = new List<long>();

        public IList<ProductAttributeVm> Attributes { get; set; } = new List<ProductAttributeVm>();

        public IList<ProductOptionVm> Options { get; set; } = new List<ProductOptionVm>();

        public IList<ProductVariationVm> Variations { get; set; } = new List<ProductVariationVm>();

        public string ThumbnailImageUrl { get; set; }

        public IList<ProductMediaVm> ProductImages { get; set; } = new List<ProductMediaVm>();

        public IList<ProductMediaVm> ProductDocuments { get; set; } = new List<ProductMediaVm>();

        public IList<long> DeletedMediaIds { get; set; } = new List<long>();

        public List<ProductLinkVm> RelatedProducts { get; set; } = new List<ProductLinkVm>();

    }
}
