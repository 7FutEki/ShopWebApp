using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Interfaces;
using ShopWebApp.Models;
using ShopWebApp.Repository;
using ShopWebApp.Services;
using ShopWebApp.ViewModel;

namespace ShopWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IPhotoService _photoService;

        public AdminController(IProductRepository productRepository, IAdminRepository adminRepository, IPhotoService photoService)
        {
            _productRepository = productRepository;
            _adminRepository = adminRepository;
            _photoService = photoService;
        }

        public async Task<IActionResult> Detail(int id)
        {
            var admin = await _adminRepository.GetAdminByIdAsync(id);
            return View(admin);
        }

        public async  Task<IActionResult> Index(int id)
        {
            var products = await _productRepository.GetAllProductAsync();
            var adminandlistproducts = new AdminAndListProduct
            {
                Id = id,
                Products = products
            };
            return View(adminandlistproducts);
        }


        public async Task<IActionResult> EditAccount(int id)
        {
            var user = await _adminRepository.GetAdminByIdAsync(id);
            if (user == null)
            {
                ModelState.AddModelError("", "Ошибка");
                return RedirectToAction("Index");
            }
            var adminVM = new EditAdminVM
            {
                Id = id,
                Login = user.Login,
                Password = user.Password,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                ImageUrl = user.Image,

            };
            return View(adminVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditAccount(int id, EditAdminVM adminVm)
        {

            var userForPhoto = await _adminRepository.GetAdminByIdAsyncNoTracking(id);
            if (userForPhoto != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userForPhoto.Image);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ошибка, не получилось удалить фотографию из облака");
                }
                var photoresult = await _photoService.AddPhotoAsync(adminVm.Image);
                var newAdmin = new Admin
                {
                    Id = id,
                    Name = adminVm.Name,
                    Email = adminVm.Email,
                    Surname = adminVm.Surname,
                    Login = adminVm.Login,
                    Password = adminVm.Password,
                    Image = photoresult.Url.ToString()
                };
                _adminRepository.Update(newAdmin);
                return RedirectToAction("Detail", "Admin", new {id = id});
            }
            else return View(adminVm);
        }
    }
}
