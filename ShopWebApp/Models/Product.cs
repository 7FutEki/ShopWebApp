using System.ComponentModel.DataAnnotations;

namespace ShopWebApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Manufacturer { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
    }
}
