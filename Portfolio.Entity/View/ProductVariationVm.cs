using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Entity.View
{
    public class ProductVariationVm
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public string Sku { get; set; }

        public string Gtin { get; set; }



    
        [BindNever]
        public IFormFile ThumbnailImage { get; set; }

        public string ThumbnailImageUrl { get; set; }

        [BindNever]
        public IList<IFormFile> NewImages { get; set; } = new List<IFormFile>();

        public IList<string> ImageUrls { get; set; } = new List<string>();

        public IList<ProductOptionCombinationVm> OptionCombinations { get; set; } =
            new List<ProductOptionCombinationVm>();
    }
}
