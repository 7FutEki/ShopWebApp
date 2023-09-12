namespace ShopWebApp.ViewModel
{
    public class CreateAdminVM
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public IFormFile? Image { get; set; }
    }
}
