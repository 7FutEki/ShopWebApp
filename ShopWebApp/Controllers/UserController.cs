using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Interfaces;
using ShopWebApp.Models;
using ShopWebApp.ViewModel;

namespace ShopWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPhotoService _photoService;

        public UserController(IUserRepository userRepository, IProductRepository productRepository, IPhotoService photoService)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _photoService = photoService;
        }
        public async Task<ActionResult> Index(int id)
        {
            
            var products = await _productRepository.GetAllProductAsync();
            var userandlistproducts = new UserAndListProduct
            {
                Id = id,
                Products = products
            };
            return View(userandlistproducts);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return View(user);
        }


        public async Task<IActionResult> EditAccount(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if(user == null)
            {
                ModelState.AddModelError("", "Ошибка");
                return RedirectToAction("Index");
            }
            var userVM = new EditUserVM
            {
                Id=id,
                Login = user.Login,
                Password = user.Password,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                ImageUrl = user.Image,

            };
            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditAccount(int id, EditUserVM userVm)
        {
            
            var userForPhoto = await _userRepository.GetUserByIdAsyncNoTracking(id);
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
                var photoresult = await _photoService.AddPhotoAsync(userVm.Image);
                var newUser = new User
                {
                    Id = id,
                    Name = userVm.Name,
                    Email = userVm.Email,
                    Surname = userVm.Surname,
                    Login = userVm.Login,
                    Password = userVm.Password,
                    Image = photoresult.Url.ToString()
                };
                _userRepository.Update(newUser);
                return RedirectToAction("Detail", "User", new {id=id});
            }
            else return View(userVm);
        }



    }
}
