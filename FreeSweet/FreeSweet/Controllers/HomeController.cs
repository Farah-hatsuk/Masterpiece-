using System;
using System.Diagnostics;
using FreeSweet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreeSweet.Controllers
{
    public class HomeController : Controller
    {

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly MyDbContext _context;

        public HomeController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.Take(4).ToList();
            var recipes = _context.Recipes.Take(4).ToList();
            var viewModel = new ProductRecipeViewModel
            {
                Products = products,
                Recipes = recipes
            };

            return View(viewModel);
        }

        //[HttpPost]
        //public IActionResult SpecialOrder(SecialOrder secialOrder, IFormFile image)
        //{
        //    if (image != null)
        //    {
        //        string fileName = Path.GetFileName(image.FileName);


        //        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/specialOrder", fileName);

        //        using (var stream = new FileStream(path, FileMode.Create))
        //        {
        //            image.CopyTo(stream);

        //        }
        //        secialOrder.Img = fileName;

        //    }
        //    if (ModelState.IsValid)
        //    {
        //        secialOrder.Id = 0;
        //        _context.SecialOrders.Add(secialOrder);
        //        _context.SaveChanges();
        //        return RedirectToAction("Index", "Home");
        //    }
        //    //return View(secialOrder);
        //    return RedirectToAction("Index", "Home");
        //}

        [HttpPost]
        public async Task<IActionResult> SpecialOrder(SecialOrder secialOrder, IFormFile image)
        {

            string? imagePath = null;

            // Handle image upload
            if (image != null)
            {
                string fileName = Path.GetFileName(image.FileName);

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/specialOrder", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(stream); // <-- use await here
                }
                secialOrder.Img = fileName;
            }

            _context.SecialOrders.Add(secialOrder);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Your special order has been submitted!";
            return RedirectToAction("Index");


          
        }




        public IActionResult Info()
        {
            return View();
        }


        public IActionResult AboutUs()
        {
            return View();
        }



        public IActionResult Recipes()
        {
            return View(_context.Recipes.ToList());
        }

       
        public IActionResult DetailsRecipes(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var racipes = _context.Recipes.FirstOrDefault(x => x.Id == id);
            if (racipes == null)
            {
                return NotFound();
            }

            return View(racipes);

        }

        

        public IActionResult Shop(int? Id)
        {
            ViewBag.SelectedCategoryId = Id;
            if (Id == null)
            {
                return View(_context.Products.ToList());
            }
            else
            {
                var cakeCategory = _context.Categories.FirstOrDefault(c => c.Id == Id);
                if (cakeCategory == null)
                {
                    return NotFound();
                }
                var products = _context.Products.Where(p => p.CategoryId == cakeCategory.Id).ToList();

                return View(products);
            }
        }

        public IActionResult Products(int id)
        {
            var products = _context.Products
                      .Include(p => p.Category)
                      .FirstOrDefault(p => p.Id == id);

            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products
                .FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }




        public IActionResult CountactUs()
        {
            

            return View();

        }

        [HttpPost]
        public IActionResult CountactUs(Contect contect)
        {
            if (ModelState.IsValid)
            {
              
                _context.Contects.Add(contect);
                 _context.SaveChanges();

                return View();
            }

            return View("Contact", contect);
           
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
