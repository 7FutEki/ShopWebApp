namespace ShopWebApp.Models
{
    public class AdminAndListProduct
    {
        public int Id { get;set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
