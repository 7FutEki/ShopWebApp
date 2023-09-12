using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Interfaces;
using ShopWebApp.Models;
using ShopWebApp.ViewModel;
using System.Diagnostics;

namespace ShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPhotoService _photoService;
        private readonly IAdminRepository _adminRepository;

        public HomeController(IUserRepository userRepository, IPhotoService photoService, IAdminRepository adminRepository)
        {
            _userRepository = userRepository;
            _photoService = photoService;
            _adminRepository = adminRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(CreateUserVM userVm)
        {
            if (ModelState.IsValid)
            {
                var userCheck = await _userRepository.GetUserByLoginAsync(userVm.Login);
                if (userCheck == null)
                {

                    var user = new User
                    {
                        Login = userVm.Login,
                        Password = userVm.Password
                    };
                    _userRepository.Add(user);
                    var userid = _userRepository.GetUserByLoginAsync(user.Login);
                    return RedirectToAction("Index", "User", new {id = userid.Result.Id});
                }
                else
                {
                    ModelState.AddModelError("", "Ошибка");
                    return View(userVm);
                }
            }
            else return View(userVm);
        }


        [HttpPost]
        public async Task<IActionResult> Index(ForAuth auth)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.GetUserByLoginAsync(auth.Login);
                var admin = await _adminRepository.GetAdminByLoginAsync(auth.Login);
                
                if (user != null)
                {
                    auth.Id = user.Id;
                    if (user.Password == auth.Password) return RedirectToAction($"Index", "User", new { id = auth.Id });
                    /*return RedirectToAction("Index", "User", auth.Id);*/
                    else
                    {
                        TempData["Error"] = "Ошибка";
                        return View(auth);
                    }
                }
                if (admin != null)
                {
                    auth.Id = admin.Id;
                    if (admin.Password == auth.Password) return RedirectToAction($"Index", "Admin", new { id = auth.Id });
                    else
                    {
                        TempData["Error"] = "Ошибка";
                        return View(auth);
                    }
                }
            }

            TempData["Error"] = "Ошибка";
            return View(auth);
        }

        
    }
}