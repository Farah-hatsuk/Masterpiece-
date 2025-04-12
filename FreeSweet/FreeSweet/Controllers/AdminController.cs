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
            //ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct(Product product , IFormFile image1 , IFormFile image2 , IFormFile image3 , IFormFile image4)
        {
            if (image1 != null && image2 != null && image3 != null && image4 != null)
            {
                string fileName1 = Path.GetFileName(image1.FileName);
                string fileName2 = Path.GetFileName(image2.FileName);
                string fileName3= Path.GetFileName(image3.FileName);
                string fileName4 = Path.GetFileName(image4.FileName);


                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Products_images", fileName1 , fileName2 , fileName3 , fileName4);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    image1.CopyTo(stream);
                    image2.CopyTo(stream);
                    image3.CopyTo(stream);
                    image4.CopyTo(stream);
                }
                product.Img1 = fileName1;
                product.Img2 = fileName2;
                product.Img3 = fileName3;
                product.Img4 = fileName4;
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
