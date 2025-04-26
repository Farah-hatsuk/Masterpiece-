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


        //public IActionResult Cart()
        //{
        //string userId = "1";

        //var cart = _context.Carts
        //    .Where(c => c.UsersId == userId)
        //    .Include(c => c.CartItem)
        //    .ThenInclude(ci => ci.Product)
        //    .FirstOrDefault();

        //return View(cart);
        //}




        //[HttpPost]

        //public async Task<IActionResult> AddToCart(int productId, string size)
        //{
        //    var product = await _context.Products.FindAsync(productId);
        //    if (product == null)
        //        return NotFound();

        //    // نفترض إنو عندك طريقة تجيب المستخدم الحالي
        //    int userId = 2;

        //    var cart = _context.Carts.FirstOrDefault(c => c.UsersId == userId);

        //    if (cart == null)
        //    {
        //        cart = new Cart
        //        {
        //            UsersId = userId,
        //            Quantity = 0,
        //            TotalPrice = 0
        //        };
        //        _context.Carts.Add(cart);
        //        await _context.SaveChangesAsync(); // لحفظ الـ cart أولاً لأنو لسه ما إله ID
        //    }

        //    var existingItem = _context.CartItems.FirstOrDefault(ci => ci.CartId == cart.Id && ci.ProductId == productId && ci.Size == size);

        //    if (existingItem != null)
        //    {
        //        existingItem.Quantity += 1;
        //        existingItem.TotalPrice += product.Price;
        //    }
        //    else
        //    {
        //        CartItem newItem = new CartItem
        //        {
        //            ProductId = product.Id,
        //            CartId = cart.Id,
        //            Quantity = 1,
        //            Size = size,
        //            Price = product.Price,
        //            TotalPrice = product.Price
        //        };
        //        _context.CartItems.Add(newItem);
        //    }

        //    // تحديث بيانات الكارت نفسه
        //    cart.Quantity += 1;
        //    //cart.TotalPrice += (double)product.Price ;
        //    cart.TotalPrice =100;

        //    await _context.SaveChangesAsync();

        //    return RedirectToAction();
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddToCart(int productId, string size, int quantity = 1, string? notes = null)
        //{
        //    var product = await _context.Products.FindAsync(productId);
        //    if (product == null)
        //        return NotFound();

        //    // ✅ جلب userId من الـ Session
        //    int? userId = HttpContext.Session.GetInt32("userId");
        //    if (userId == null)
        //    {
        //        return RedirectToAction("RegistrationLogin", "User"); // أو أي صفحة تسجيل دخول عندك
        //    }

        //    var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UsersId == userId);

        //    if (cart == null)
        //    {
        //        cart = new Cart
        //        {
        //            UsersId = userId,
        //            Quantity = 0,
        //            TotalPrice = 0
        //        };
        //        _context.Carts.Add(cart);
        //        await _context.SaveChangesAsync(); // ضروري لحفظ ID الكارت
        //    }

        //    var existingItem = await _context.CartItems
        //        .FirstOrDefaultAsync(ci => ci.CartId == cart.Id && ci.ProductId == productId && ci.Size == size);

        //    if (existingItem != null)
        //    {
        //        existingItem.Quantity += quantity;
        //        existingItem.TotalPrice += product.Price * quantity;
        //    }
        //    else
        //    {
        //        CartItem newItem = new CartItem
        //        {
        //            ProductId = product.Id,
        //            CartId = cart.Id,
        //            Quantity = quantity,
        //            Size = size,
        //            Price = product.Price,
        //            TotalPrice = product.Price * quantity
        //        };
        //        _context.CartItems.Add(newItem);
        //    }

        //    cart.Quantity += quantity;
        //    cart.TotalPrice += (int?)(product.Price * quantity);

        //    await _context.SaveChangesAsync();

        //    return RedirectToAction("Products", "Home"); // بدليها باسم صفحة السلة عندك
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddToCart(int productId, string size, int quantity = 1)
        //{
        //    // تأكد من أن المنتج موجود
        //    var product = await _context.Products.FindAsync(productId);
        //    if (product == null)
        //        return NotFound("المنتج غير موجود");

        //    // الحصول على userId من السيشن
        //    var userId = HttpContext.Session.GetInt32("userId");
        //    if (userId == null)
        //        return RedirectToAction("Login", "Account");

        //    // الحصول على السلة الحالية أو إنشاؤها
        //    var cart = _context.Carts.FirstOrDefault(c => c.UsersId == userId);
        //    if (cart == null)
        //    {
        //        cart = new Cart
        //        {
        //            UsersId = userId,
        //            Quantity = 0,
        //            TotalPrice = 0
        //        };
        //        _context.Carts.Add(cart);
        //        await _context.SaveChangesAsync();
        //    }

        //    // التحقق من وجود العنصر مسبقًا
        //    var existingItem = _context.CartItems.FirstOrDefault(ci =>
        //        ci.CartId == cart.Id &&
        //        ci.ProductId == product.Id &&
        //        ci.Size == size
        //    );

        //    if (existingItem != null)
        //    {
        //        existingItem.Quantity += quantity;
        //        existingItem.TotalPrice += product.Price * quantity;
        //    }
        //    else
        //    {
        //        var cartItem = new CartItem
        //        {
        //            ProductId = product.Id,
        //            CartId = cart.Id,
        //            Quantity = quantity,
        //            Size = size,
        //            Price = product.Price,
        //            TotalPrice = product.Price * quantity
        //        };
        //        _context.CartItems.Add(cartItem);
        //    }

        //    // تحديث بيانات السلة
        //    cart.Quantity += quantity;
        //    cart.TotalPrice += (int?)(product.Price * quantity);

        //    await _context.SaveChangesAsync();

        //    return RedirectToAction("Home", "Products");
        //}

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
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                return NotFound();
            if(size == "large")
            {
                product.Price = 30;
            }else if (size == "medium")
            {
                product.Price = 20;
            }else if (size == "small")
            {
                product.Price = 15;
            }else if (size == "mini")
            {
                product.Price = 5;
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

            if (existingItem != null)
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
                    Price = product.Price,
                    TotalPrice = (product.Price * quantity )
                };
                _context.CartItems.Add(newItem);
            }

            // 5. Update cart totals
            cart.Quantity += 1;
            cart.TotalPrice += (int)(product.Price * quantity);

            await _context.SaveChangesAsync();

            return RedirectToAction("Shop", "Home"); // Or wherever you want to go after
        }


    }
}
