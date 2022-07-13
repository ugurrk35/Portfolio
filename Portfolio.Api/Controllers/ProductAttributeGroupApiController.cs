using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Data.Abstract.BaseRepository;
using Portfolio.Entity.Models;
using Portfolio.Entity.View;
using System.Linq;

namespace Portfolio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAttributeGroupApiController : ControllerBase
    {
        private IRepository<ProductAttributeGroup> _productAttrGroupRepository;

        public ProductAttributeGroupApiController(IRepository<ProductAttributeGroup> productAttrGroupRepository)
        {
            _productAttrGroupRepository = productAttrGroupRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var attributeGroups = _productAttrGroupRepository
                .Query()
                .Select(x => new ProductAttributeGroupFormVm
                {
                    Id = x.Id,
                    Name = x.Name
                });

            return Ok(attributeGroups);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var productAttributeGroup = _productAttrGroupRepository.Query().FirstOrDefault(x => x.Id == id);
            var model = new ProductAttributeGroupFormVm
            {
                Id = productAttributeGroup.Id,
                Name = productAttributeGroup.Name
            };

            return Ok(model);
        }

        [HttpPost]

        public IActionResult Post([FromBody] ProductAttributeGroupFormVm model)
        {
            if (ModelState.IsValid)
            {
                var productAttributeGroup = new ProductAttributeGroup
                {
                    Name = model.Name
                };

                _productAttrGroupRepository.Add(productAttributeGroup);
                _productAttrGroupRepository.SaveChanges();

                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
   
        public IActionResult Put(long id, [FromBody] ProductAttributeGroupFormVm model)
        {
            if (ModelState.IsValid)
            {
                var productAttributeGroup = _productAttrGroupRepository.Query().FirstOrDefault(x => x.Id == id);
                productAttributeGroup.Name = model.Name;

                _productAttrGroupRepository.SaveChanges();

                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
    
        public IActionResult Delete(long id)
        {
            var productAttributeGroup = _productAttrGroupRepository.Query().FirstOrDefault(x => x.Id == id);
            if (productAttributeGroup == null)
            {
                return NotFound();
            }

            _productAttrGroupRepository.Remove(productAttributeGroup);
            return Ok(true);
        }
    }
}
