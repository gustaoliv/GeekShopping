using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Messages;
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
        public async Task<ActionResult<CartVO>> FindById(string id)
        {
            var cartVO = await _repository.FindCartByUserId(id);
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

        [HttpPost("apply-coupon")]
        public async Task<ActionResult<CartVO>> ApplyCoupon([FromBody] CartVO vo)
        {
            var status = await _repository.ApplyCoupon(vo.CartHeader.UserId, vo.CartHeader.CouponCode);
            if (!status)
                return BadRequest();
            else
                return Ok(status);
        }

        [HttpDelete("remove-coupon/{userId}")]
        public async Task<ActionResult<CartVO>> RemoveCoupon(string userId)
        {
            var status = await _repository.RemoveCoupon(userId);
            if (!status)
                return BadRequest();
            else
                return Ok(status);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderVO>> Checkout(CheckoutHeaderVO vo)
        {
            var cartVO = await _repository.FindCartByUserId(vo.UserId);
            if (cartVO == null)
                return NotFound();

            vo.CartDetails  = cartVO.CartDetails;
            vo.DateTime     = DateTime.Now;
            //TASK RabbitMQ logic comes here!

            return Ok(vo);
        }
    }
}
