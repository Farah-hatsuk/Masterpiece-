using FreeSweet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreeSweet.Controllers
{
    

    public class CartController : Controller
    {

        private readonly MyDbContext _context;

        public CartController(MyDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, string size , int quantity )
        
        {
            
            // 1. Get user ID from session
            int? userId = HttpContext.Session.GetInt32("userId");
            if (userId == null)
            {
                return RedirectToAction("RegistrationLogin", "User"); // Or however you handle auth
            }

            // 2. Find the product

            // ❗ جلب المنتج مع الـ Category
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
                return NotFound();

            if (size == null)
            {
                size = product.Size;
            }

            var p_price=product.Price;
            if (product.Category != null && product.Category.Name == "Cake")
            {
                if (size == "large")
                {
                    p_price = 30;
                }
                else if (size == "medium")
                {
                    p_price = 20;
                }
                else if (size == "small")
                {
                    p_price = 15;
                }
                else if (size == "mini")
                {
                    p_price = 5;
                }
                else
                {
                    size = "mini";
                    p_price = 5;
                }
            }


            // 3. Get or create cart for user
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UsersId == userId);
            if (cart == null)
            {
                cart = new Cart
                {
                    UsersId = userId,
                    Quantity = 0,
                    TotalPrice = 0
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync(); // Save to get the cart ID
            }

            // 4. Check if item already exists in the cart
            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cart.Id && ci.ProductId == product.Id && ci.Size == size);

            if (existingItem != null && existingItem.Size == size)
            {
                existingItem.Quantity += quantity;
                existingItem.TotalPrice = (product.Price * existingItem.Quantity) ;
            }
            else
            {
                CartItem newItem = new CartItem
                {
                    ProductId = product.Id,
                    CartId = cart.Id,
                    Quantity = quantity,
                    Size = size,
                    Price = p_price,
                    TotalPrice = (p_price * quantity )
                };
                _context.CartItems.Add(newItem);
            }

            // 5. Update cart totals
            cart.Quantity += 1;
            cart.TotalPrice += (p_price * quantity );

            await _context.SaveChangesAsync();

            TempData["AddToCartSuccess"] = "Product added to your cart!";
            return RedirectToAction("Shop", "Home"); // Or wherever you want to go after
        }


        public IActionResult Index()
        {

            int? userId = HttpContext.Session.GetInt32("userId");

            var cart = _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product) // Assuming you have a Product relation
                .Where(x => x.UsersId == userId).FirstOrDefault(); // أو حسب الـ UserId لو بدك كارت لمستخدم محدد


            return View(cart);
        }

        public async Task<IActionResult> DeleteItem( int id)
        {
            var item = _context.CartItems.SingleOrDefault(i => i.Id == id);

            if (item == null)
            {
                return RedirectToAction("Index");
            }

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}
