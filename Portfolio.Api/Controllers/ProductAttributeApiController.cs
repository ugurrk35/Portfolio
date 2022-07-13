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
    public class ProductAttributeApiController : ControllerBase
    {
        private readonly IRepository<ProductAttribute> _productAttrRepository;
        public ProductAttributeApiController(IRepository<ProductAttribute> productAttrRepository)
        {
            _productAttrRepository = productAttrRepository;
        }
        [HttpGet]
        public IActionResult List()
        {
            var attributes = _productAttrRepository
                .Query()
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    GroupName = x.Group.Name
                });

            return Ok(attributes);
        }
        [HttpPost]
        public IActionResult Post([FromBody] ProductAttributeFormVm model)
        {
            if (ModelState.IsValid)
            {
                var productAttribute = new ProductAttribute
                {
                    Name = model.Name,
                    GroupId = model.GroupId
                };

                _productAttrRepository.Add(productAttribute);
                _productAttrRepository.SaveChanges();

                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]       
        public IActionResult Put(long id, [FromBody] ProductAttributeFormVm model)
        {
            if (ModelState.IsValid)
            {
                var productAttribute = _productAttrRepository.Query().FirstOrDefault(x => x.Id == id);
                productAttribute.Name = model.Name;
                productAttribute.GroupId = model.GroupId;

                _productAttrRepository.SaveChanges();

                return Ok();
            }

            return BadRequest(ModelState);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var productAttribute = _productAttrRepository.Query().FirstOrDefault(x => x.Id == id);
            if (productAttribute == null)
            {
                return NotFound();
            }

            _productAttrRepository.Remove(productAttribute);
            return Ok(true);
        }
    }
}
