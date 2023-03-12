using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _repository;

        public CartController(ICartRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartVO>> FindById(string userId)
        {
            CartVO cartVO = await _repository.FindCartByUserId(userId);
            if (cartVO == null)
                return NotFound();
            else
                return Ok(cartVO);
        }

        [HttpPost("add-cart")]
        public async Task<ActionResult<CartVO>> AddCart([FromBody] CartVO vo)
        {
            CartVO cartVO = await _repository.SaveOrUpdateCart(vo);
            if (cartVO == null)
                return BadRequest();
            else
                return Ok(cartVO);
        }

        [HttpPut("update-cart")]
        public async Task<ActionResult<CartVO>> UpdateCart([FromBody] CartVO vo)
        {
            CartVO cartVO = await _repository.SaveOrUpdateCart(vo);
            if (cartVO == null)
                return NotFound();
            else
                return Ok(cartVO);
        }

        [HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<CartVO>> RemoveCart(int id)
        {
            bool status = await _repository.RemoveFromCart(id);
            if (!status)
                return BadRequest();
            else
                return Ok(status);
        }
    }
}
