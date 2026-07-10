namespace Basket_API.Models
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
        public ShoppingCart(string userName)
        {
            userName = userName;
        }
        //Required for mapping
        public ShoppingCart()
        {
            
        }
    }
}
