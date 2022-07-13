using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Entity.View
{
    public class ProductForm
    {
        public ProductVm Product { get; set; } = new ProductVm();

        public IFormFile ThumbnailImage { get; set; }

        public IList<IFormFile> ProductImages { get; set; } = new List<IFormFile>();

        public IList<IFormFile> ProductDocuments { get; set; } = new List<IFormFile>();

    }
}
