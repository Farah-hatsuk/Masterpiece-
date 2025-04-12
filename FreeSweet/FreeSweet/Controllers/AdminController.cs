using FreeSweet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FreeSweet.Controllers
{
    public class AdminController : Controller
    {
        private readonly MyDbContext _context;

        public AdminController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Product()
        {
            return View(_context.Products.ToList());
        }

        public IActionResult CreateProduct()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct(Product product , IFormFile image)
        {
            if (image != null)
            {
                string fileName = Path.GetFileName(image.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Products_images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                product.Img1 = fileName;
                product.Img2 = fileName;
                product.Img3 = fileName;
                product.Img4 = fileName;
            }
            if (ModelState.IsValid)
            {
                product.Id = 0;
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("", "Admin");
            }
            return View();
        }



        public IActionResult EditProduct() 
        {
            return View();
        }

        public IActionResult DeleteProduct() 
        {
            return View();
        }
    }
}
