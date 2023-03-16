using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Messages;
using GeekShopping.CartAPI.RabbitMQSender;
using GeekShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly IRabbitMQMessageSender _rabbitMQMessageSender;
        public CartController(ICartRepository repository, IRabbitMQMessageSender rabbitMQMessageSender, ICouponRepository couponRepository)
        {
            _cartRepository = repository;
            _rabbitMQMessageSender = rabbitMQMessageSender;
            _couponRepository = couponRepository;
        }

        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartVO>> FindById(string id)
        {
            var cartVO = await _cartRepository.FindCartByUserId(id);
            if (cartVO == null)
                return NotFound();
            else
                return Ok(cartVO);
        }

        [HttpPost("add-cart")]
        public async Task<ActionResult<CartVO>> AddCart([FromBody] CartVO vo)
        {
            CartVO cartVO = await _cartRepository.SaveOrUpdateCart(vo);
            if (cartVO == null)
                return BadRequest();
            else
                return Ok(cartVO);
        }

        [HttpPut("update-cart")]
        public async Task<ActionResult<CartVO>> UpdateCart([FromBody] CartVO vo)
        {
            CartVO cartVO = await _cartRepository.SaveOrUpdateCart(vo);
            if (cartVO == null)
                return NotFound();
            else
                return Ok(cartVO);
        }

        [HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<CartVO>> RemoveCart(int id)
        {
            bool status = await _cartRepository.RemoveFromCart(id);
            if (!status)
                return BadRequest();
            else
                return Ok(status);
        }

        [HttpPost("apply-coupon")]
        public async Task<ActionResult<CartVO>> ApplyCoupon([FromBody] CartVO vo)
        {
            var status = await _cartRepository.ApplyCoupon(vo.CartHeader.UserId, vo.CartHeader.CouponCode);
            if (!status)
                return BadRequest();
            else
                return Ok(status);
        }

        [HttpDelete("remove-coupon/{userId}")]
        public async Task<ActionResult<CartVO>> RemoveCoupon(string userId)
        {
            var status = await _cartRepository.RemoveCoupon(userId);
            if (!status)
                return BadRequest();
            else
                return Ok(status);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderVO>> Checkout(CheckoutHeaderVO vo)
        {
            string token = await HttpContext.GetTokenAsync("access_token");

            if (vo?.UserId == null)
                return BadRequest();

            var cartVO = await _cartRepository.FindCartByUserId(vo.UserId);
            if (cartVO == null)
                return NotFound();

            if (!String.IsNullOrEmpty(vo.CouponCode))
            {
                CouponVO coupon = await _couponRepository.GetCouponByCouponCode(vo.CouponCode, token);
                if(vo.DiscountTotal != coupon.DiscountAmount)
                {
                    return StatusCode(412);
                }

            }

            vo.CartDetails  = cartVO.CartDetails;
            vo.DateTime     = DateTime.Now;

            _rabbitMQMessageSender.SendMessage(vo, "checkoutQueue");

            await _cartRepository.ClearCart(vo.UserId);

            return Ok(vo);
        }
    }
}
