using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Interfaces;
using ShopWebApp.Models;
using ShopWebApp.ViewModel;

namespace ShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        private readonly IPhotoService _photoService;
        private readonly IUserRepository _userRepository;

        public ProductController(IProductRepository productRepository, IPhotoService photoService, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _photoService = photoService;
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Index(int id)
        {
            var products = await _productRepository.GetAllProductAsync();
            var adminandlistproducts = new AdminAndListProduct
            {
                Id = id,
                Products = products
            };
            return View(adminandlistproducts);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            return View(product);
        }

        public IActionResult Create(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id, CreateProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var photoResult = await _photoService.AddPhotoAsync(productVM.Image);
                var product = new Product
                {
                    Title = productVM.Title,
                    Description = productVM.Description,
                    Price = productVM.Price,
                    Image = photoResult.Url.ToString(),
                    Manufacturer = productVM.Manufacturer
                };
                _productRepository.Add(product);
                return RedirectToAction($"Index", "Admin", new {id = id});
            }
            else
            {
                ModelState.AddModelError("", "Ошибка. Попробуйте позже");
                return View(productVM);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if(product == null) return View("Ошибка");
            var productVm = new EditProductVM
            {
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.Image,
                Manufacturer = product.Manufacturer
            };
            return View(productVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditProductVM productVM)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Не получилось обновить товар");
                return View("Edit", productVM);
            }
            var userProduct = await _productRepository.GetProductByIdAsyncNoTracking(id);
            if (userProduct != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userProduct.Image);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Не получилось обновить фотографию");
                    return View(productVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(productVM.Image);
                var product = new Product
                {
                    Id = id,
                    Title = productVM.Title,
                    Description = productVM.Description,
                    Price = productVM.Price,
                    Image = photoResult.Url.ToString(),
                    Manufacturer = productVM.Manufacturer
                };
                _productRepository.Update(product);
                return RedirectToAction("Index", "Home");
            }
            else return View(productVM);
        }


        public async Task<IActionResult> Delete(int id, EditProductVM productVM)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product != null)
            {
                try
                {
                    _photoService.DeletePhotoAsync(product.Image);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Не получилось обновить фотографию");
                    return View(product);
                }
                _productRepository.Delete(product);
                
                return RedirectToAction("Index", "Home");
            }
            else return View("Error");
        }
    }
}
