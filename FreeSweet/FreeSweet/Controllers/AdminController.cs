using System.IO;
using System.Text.RegularExpressions;
using FreeSweet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            ViewBag.TotalOrders = _context.Orders.Count();
            ViewBag.Revenue = _context.Orders.Sum(o => o.TotalAmount);
            ViewBag.TotalCustomers = _context.Users.Count();
            ViewBag.TotalProducts = _context.Products.Count();


            return View();
        }



        public IActionResult Product(int? Id)
        {
            //return View(_context.Products.ToList());
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

        public IActionResult ProductDetalis(int id)
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

        public IActionResult CreateProduct()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }


        [HttpPost]
        public IActionResult CreateProduct(Product product, IFormFile image1, IFormFile image2, IFormFile image3, IFormFile image4)
        {
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/shop/shop");

            if (image1 != null)
            {
                string fileName1 = Path.GetFileName(image1.FileName);
                string path1 = Path.Combine(uploadsFolder, fileName1);
                using (var stream = new FileStream(path1, FileMode.Create))
                {
                    image1.CopyTo(stream);
                }
                product.Img1 = fileName1;
            }

            if (image2 != null)
            {
                string fileName2 = Path.GetFileName(image2.FileName);
                string path2 = Path.Combine(uploadsFolder, fileName2);
                using (var stream = new FileStream(path2, FileMode.Create))
                {
                    image2.CopyTo(stream);
                }
                product.Img2 = fileName2;
            }

            if (image3 != null)
            {
                string fileName3 = Path.GetFileName(image3.FileName);
                string path3 = Path.Combine(uploadsFolder, fileName3);
                using (var stream = new FileStream(path3, FileMode.Create))
                {
                    image3.CopyTo(stream);
                }
                product.Img3 = fileName3;
            }

            if (image4 != null)
            {
                string fileName4 = Path.GetFileName(image4.FileName);
                string path4 = Path.Combine(uploadsFolder, fileName4);
                using (var stream = new FileStream(path4, FileMode.Create))
                {
                    image4.CopyTo(stream);
                }
                product.Img4 = fileName4;
            }

            
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Product", "Admin");
         

         
        }

        public IActionResult EditProduct(int? id ) 
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.DepName = product.Name;
            ViewBag.DepDes = product.Description;
            ViewBag.DepDes = product.Price;
            ViewBag.DepDes = product.Size;
            ViewBag.DepImage1 = product.Img1;
            ViewBag.DepImage2 = product.Img2;
            ViewBag.DepImage3 = product.Img3;
            ViewBag.DepImage4 = product.Img4;
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View(product);
        }
        [HttpPost]
        public IActionResult EditProduct(Product product, IFormFile Img1, IFormFile Img2, IFormFile Img3, IFormFile Img4)
        {
            var Pro = _context.Products.Find(product.Id);


            if (Img1 != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/shop/shop");

                // صورة 1 - إلزامية
                string fileName1 = Path.GetFileName(Img1.FileName);
                string path1 = Path.Combine(uploadsFolder, fileName1);
                using (var stream = new FileStream(path1, FileMode.Create))
                {
                    Img1.CopyTo(stream);
                }
                Pro.Img1 = fileName1;

                // صورة 2 - اختيارية
                if (Img2 != null)
                {
                    string fileName2 = Path.GetFileName(Img2.FileName);
                    string path2 = Path.Combine(uploadsFolder, fileName2);
                    using (var stream = new FileStream(path2, FileMode.Create))
                    {
                        Img2.CopyTo(stream);
                    }
                    Pro.Img2 = fileName2;
                }

                // صورة 3 - اختيارية
                if (Img3 != null)
                {
                    string fileName3 = Path.GetFileName(Img3.FileName);
                    string path3 = Path.Combine(uploadsFolder, fileName3);
                    using (var stream = new FileStream(path3, FileMode.Create))
                    {
                        Img3.CopyTo(stream);
                    }
                    Pro.Img3 = fileName3;
                }

                // صورة 4 - اختيارية
                if (Img4 != null)
                {
                    string fileName4 = Path.GetFileName(Img4.FileName);
                    string path4 = Path.Combine(uploadsFolder, fileName4);
                    using (var stream = new FileStream(path4, FileMode.Create))
                    {
                        Img4.CopyTo(stream);
                    }
                    Pro.Img4 = fileName4;
                }
            }

           
                Pro.Name = product.Name;
                Pro.Description = product.Description;
                Pro.Price = product.Price;
                Pro.Size = product.Size;
          
                _context.Products.Update(Pro);
                _context.SaveChanges();
                return RedirectToAction("Product", "Admin");
        }

        [HttpPost]
        public IActionResult DeleteProduct(int id) 
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Product", "Admin");
        }

        //category 
        public IActionResult Category()
        {
            {
                return View(_context.Categories.ToList());
            }
        }
        public IActionResult CreateCategory()
        {
            return View();
        }





        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Id = 0;
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Category", "Admin");
            }
            return View(category);
        }

        public IActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewBag.CategoryName = category.Name;

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View(category);
        }

        [HttpPost]
        public IActionResult EditCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(category);
                _context.SaveChanges();
                return RedirectToAction("Category", "Admin");
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Category", "Admin");
        }


        //recipe

        public IActionResult Recipe()
        {
            return View(_context.Recipes.ToList());
        }

        public IActionResult RecipeDetalis(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = _context.Recipes
                .FirstOrDefault(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        public IActionResult CreateRecipe()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateRecipe(IFormCollection form, IFormFile image1, IFormFile image2, IFormFile image3, IFormFile image4, IFormFile image5)
        {
            Recipe recipe = new Recipe();

            // قراءة النصوص من الفورم
            recipe.Name = form["Name"];
            recipe.Description = form["Description"];
            recipe.PrepTime = form["PrepTime"];
            recipe.CookTime = form["CookTime"];
            recipe.TotalTime = form["TotalTime"];
            recipe.Ingredient = form["Ingredient"];
            recipe.Instructions = form["Instructions"]; // هنا بيكون فيه HTML
            recipe.Notes = form["Notes"];

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/shop/Reprises");

            if (image1 != null)
            {
                string fileName1 = Path.GetFileName(image1.FileName);
                string path1 = Path.Combine(uploadsFolder, fileName1);
                using (var stream = new FileStream(path1, FileMode.Create))
                {
                    image1.CopyTo(stream);
                }
                recipe.Img1 = fileName1;
            }

            if (image2 != null)
            {
                string fileName2 = Path.GetFileName(image2.FileName);
                string path2 = Path.Combine(uploadsFolder, fileName2);
                using (var stream = new FileStream(path2, FileMode.Create))
                {
                    image2.CopyTo(stream);
                }
                recipe.Img2 = fileName2;
            }

            if (image3 != null)
            {
                string fileName3 = Path.GetFileName(image3.FileName);
                string path3 = Path.Combine(uploadsFolder, fileName3);
                using (var stream = new FileStream(path3, FileMode.Create))
                {
                    image3.CopyTo(stream);
                }
                recipe.Img3 = fileName3;
            }

            if (image4 != null)
            {
                string fileName4 = Path.GetFileName(image4.FileName);
                string path4 = Path.Combine(uploadsFolder, fileName4);
                using (var stream = new FileStream(path4, FileMode.Create))
                {
                    image4.CopyTo(stream);
                }
                recipe.Img4 = fileName4;
            }


            _context.Recipes.Add(recipe);
            _context.SaveChanges();
            return RedirectToAction("Recipe", "Admin");

        }


        // دالة لتحويل النص إلى HTML List
        private string ConvertToHtmlList(string text)
        {
            var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var htmlList = "<ul>";
            foreach (var line in lines)
            {
                htmlList += $"<li>{line.Trim()}</li>";
            }
            htmlList += "</ul>";
            return htmlList;
        }




        private string ConvertFromHtmlList(string html)
        {
            if (string.IsNullOrEmpty(html))
                return "";

            var matches = Regex.Matches(html, @"<li>(.*?)</li>", RegexOptions.Singleline);
            var lines = matches.Select(m => m.Groups[1].Value.Trim()).ToList();
            return string.Join(Environment.NewLine, lines);
        }

        public IActionResult EditRecipe(int id)
        {
            var recipe = _context.Recipes.Find(id);
            if (recipe == null)
            {
                return NotFound();
            }

            // نحولهم لسطر لكل عنصر حتى يظهروا في التعديل
            recipe.Ingredient = ConvertFromHtmlList(recipe.Ingredient);
            recipe.Instructions = ConvertFromHtmlList(recipe.Instructions);
            recipe.Notes = ConvertFromHtmlList(recipe.Notes);

            return View(recipe);
        }

        [HttpPost]
        public IActionResult EditRecipe(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                // نحولهم من سطور لـ HTML قبل التخزين
                recipe.Ingredient = ConvertToHtmlList(recipe.Ingredient);
                recipe.Instructions = ConvertToHtmlList(recipe.Instructions);
                recipe.Notes = ConvertToHtmlList(recipe.Notes);

                _context.Recipes.Update(recipe);
                _context.SaveChanges();

                return RedirectToAction("Recipe");
            }
            return View(recipe);
        }

        [HttpPost]
        public IActionResult DeleteRecipe(int id)
        {
            var recipe = _context.Recipes.Find(id);

            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            _context.SaveChanges();

            return RedirectToAction("Recipe", "Admin"); 
        }


        //order
        public IActionResult SpecialOrder()
        {
            return View(_context.SecialOrders.ToList());
        }

        public IActionResult SpecialOrderDetalis(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var SpecialOrder = _context.SecialOrders.FirstOrDefault(m => m.Id == id);
            if (SpecialOrder == null)
            {
                return NotFound();
            }

            return View(SpecialOrder);
        }


        //public IActionResult Order()
        //{
        //    var orders = _context.Orders
        //    .Include(o => o.Users) 
        //    .ToList();
        //    return View(orders);
        //}

        public IActionResult Order(int page = 1, int pageSize = 15)
        {
            var ordersQuery = _context.Orders
                .Include(o => o.Users)
                .Include(o => o.Payment) // إذا أردت عرض الـ Payment أيضًا
                .OrderByDescending(o => o.Date); // ترتيب تنازلي حسب التاريخ (أو .OrderByDescending(o => o.Id))

            int totalOrders = ordersQuery.Count();

            var pagedOrders = ordersQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalOrders / pageSize);

            return View(pagedOrders);
        }


        public IActionResult OrderDetails(int id)
        {
            var order = _context.Orders
         .Include(o => o.Users)
         .Include(o => o.Payment)
         .Include(o => o.OrderItems)
             .ThenInclude(oi => oi.Product) // Include Product for each OrderItem
         .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult FeedBack()
        {
            return View(_context.Contects.ToList());
        }
    }
}
