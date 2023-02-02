using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentException(nameof(repository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        {
            IEnumerable<ProductVO> productsVO = await _repository.FindAll();
            return Ok(productsVO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVO>> FindById(long id)
        {
            ProductVO productVO  = await _repository.FindById(id);
            if (productVO.Id <= 0)
                return NotFound();
            else
                return Ok(productVO);
        }

        [HttpPost]
        public async Task<ActionResult<ProductVO>> Create(ProductVO productVO)
        {
            if (productVO == null)
                return BadRequest();

            ProductVO productVOReturn = await _repository.Create(productVO);
            return Ok(productVOReturn);
        }

        [HttpPut]
        public async Task<ActionResult<ProductVO>> Update(ProductVO productVO)
        {
            if (productVO == null)
                return BadRequest();

            ProductVO productVOReturn = await _repository.Update(productVO);
            return Ok(productVOReturn);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            if (!await _repository.Delete(id))
                return BadRequest();
            else
                return Ok();
        }
    }
}
