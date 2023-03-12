namespace GeekShopping.Web.Models
{
    public class CartViewModel
    {
        public CartHeaderVO CartHeader { get; set; }
        public IEnumerable<CartDetailVO> CartDetails { get; set; }
    }
}
