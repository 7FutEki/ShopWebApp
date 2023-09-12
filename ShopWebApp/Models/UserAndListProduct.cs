namespace ShopWebApp.Models
{
    public class UserAndListProduct
    {
        public int Id { get; set; }
        public IEnumerable<Product> Products { get; set;}
    }
}
