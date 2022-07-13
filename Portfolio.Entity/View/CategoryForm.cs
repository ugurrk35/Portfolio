using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Portfolio.Entity.View
{
    public class CategoryForm
    {
        public CategoryForm()
        {
            IsPublished = true;
        }

        public long Id { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public string Slug { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string MetaTitle { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public int DisplayOrder { get; set; }

    

        public bool IncludeInMenu { get; set; }

        public bool IsPublished { get; set; }

        public IFormFile ThumbnailImage { get; set; }

        public string ThumbnailImageUrl { get; set; }
    }
}
