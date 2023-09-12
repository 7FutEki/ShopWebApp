using System.ComponentModel.DataAnnotations;

namespace ShopWebApp.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Image { get; set; }

    }
}
