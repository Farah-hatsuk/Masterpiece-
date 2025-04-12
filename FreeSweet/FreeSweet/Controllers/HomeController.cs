using System.Diagnostics;
using FreeSweet.Models;
using Microsoft.AspNetCore.Mvc;

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
            var products = _context.Products.ToList();
            var recipes = _context.Recipes.ToList();
            var viewModel = new ProductRecipeViewModel
            {
                Products = products,
                Recipes = recipes
            };

            return View(viewModel);
        }

        public IActionResult SpecialOrder(SecialOrder secialOrder , IFormFile image)
        {
            if (image != null )
            {
                string fileName = Path.GetFileName(image.FileName);
               

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Products_images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    image.CopyTo(stream);
               
                }
                secialOrder.Img = fileName;
        
            }
            if (ModelState.IsValid)
            {
                secialOrder.Id = 0;
                _context.SecialOrders.Add(secialOrder);
                _context.SaveChanges();
                return RedirectToAction("", "Admin");
            }
            return View();
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

        //public IActionResult DetailsRecipes(int id)
        public IActionResult DetailsRecipes()
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var racipes = _context.Recipes.FirstOrDefault(x => x.Id == id);
            //if (racipes == null) 
            //{
            //    return NotFound();
            //}

            //return View(racipes);
            return View();
        }





        public IActionResult Shop()
        {
            return View(_context.Products.ToList());
        }


        //public IActionResult Products(int? id)
        public IActionResult Products()
        {
            //    if (id == null)
            //    {
            //        return NotFound();
            //    }

            //    var product = _context.Products
            //        .FirstOrDefault(m => m.Id == id);
            //    if (product == null)
            //    {
            //        return NotFound();
            //    }

            //    return View(product);
            return View();

        }

        public IActionResult CakeProducts()
        {
            var cakeCategory = _context.Categories.FirstOrDefault(c => c.Name == "Cake");
            if (cakeCategory == null)
            {
                return NotFound(); 
            }
            var products = _context.Products.Where(p => p.CategoryId == cakeCategory.Id).ToList();

            return View(products);
        }

        public IActionResult WesternProducts()
        {
            var westrenCategory = _context.Categories.FirstOrDefault(c => c.Name == "Western");
            if (westrenCategory == null)
            {
                return NotFound();
            }
            var products = _context.Products.Where(p => p.CategoryId == westrenCategory.Id).ToList();

            return View(products);
        }

        public IActionResult ArabicProducts()
        {
            var arabicCategory = _context.Categories.FirstOrDefault(c => c.Name == "Arabic");
            if (arabicCategory == null)
            {
                return NotFound();
            }
            var products = _context.Products.Where(p => p.CategoryId == arabicCategory.Id).ToList();

            return View(products);
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
