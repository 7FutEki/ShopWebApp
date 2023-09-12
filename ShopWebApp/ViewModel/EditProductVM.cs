namespace ShopWebApp.ViewModel
{
    public class EditProductVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile Image { get; set; }
        public double Price { get; set; }
    }
}
