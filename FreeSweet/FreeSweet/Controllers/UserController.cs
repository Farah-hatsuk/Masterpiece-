using FreeSweet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace FreeSweet.Controllers
{
    public class UserController : Controller
    {
        private readonly MyDbContext _context;

        public UserController(MyDbContext context)
        {
            _context = context;
        }

        //creat new user + login

        public IActionResult RegistrationLogin()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Role = "user";
            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["message"] = "Registered Successfully!";

            return RedirectToAction("RegistrationLogin");
        }


    

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var userinfo = _context.Users.FirstOrDefault(u => u.Email == user.Email);

            if (userinfo != null && BCrypt.Net.BCrypt.Verify(user.Password, userinfo.Password))
            {
                if (userinfo.Role == "admin")
                {
                    return RedirectToAction("Index", "Admin");
                }

                HttpContext.Session.SetInt32("userId", userinfo.Id);
                HttpContext.Session.SetString("UserEmail", userinfo.Email);
                await HttpContext.Session.CommitAsync();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Email or password is incorrect");
                return View("RegistrationLogin");
            }
        }

   


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }


        // profile 

        public IActionResult Profile()
        {

            var email = HttpContext.Session.GetString("UserEmail");
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            var orders = _context.Orders.Where(o => o.UsersId == user.Id).ToList(); // Assuming you have an `Orders` table

            var userOrder = new UserOrder
            {
                user = user,
                Order = orders
            };

            return View(userOrder); // Pass the correct model to the view
        }

        public IActionResult EditProfile()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return View(user);
        }

        [HttpPost]
        public IActionResult EditProfile(User UpdatUser, IFormFile profileImage)
        {
            var email = HttpContext.Session.GetString("UserEmail");
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                user.Name = UpdatUser.Name;
                user.Email = UpdatUser.Email;
                user.Phone = UpdatUser.Phone;
                user.Address = UpdatUser.Address;

                if (profileImage != null)
                {
                    string fileName = Path.GetFileName(profileImage.FileName);
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/UserImg", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        profileImage.CopyTo(stream);
                    }

                    user.Img = fileName;
                }

                _context.SaveChanges();

                HttpContext.Session.SetString("UserName", user.Name);
                HttpContext.Session.SetString("UserImg", user.Img ?? "");
                HttpContext.Session.SetString("UserPhone", user.Phone ?? "");
                HttpContext.Session.SetString("UserAddress", user.Address ?? "");

                TempData["SuccessMessage"] = "Profile updated successfully!";

                return RedirectToAction("Profile");
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult ResetPassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                TempData["ErrorMessage"] = "Passwords do not match.";
                return RedirectToAction("Profile");
            }

            string userEmail = HttpContext.Session.GetString("UserEmail");
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);

            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Profile");
            }

            if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.Password))
            {
                TempData["ErrorMessage"] = "Incorrect current password.";
                return RedirectToAction("Profile");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.Password = hashedPassword;
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Password updated successfully!";
            return RedirectToAction("Profile");
        }




        public IActionResult HistoryOfOrder()
        {
            var userId = HttpContext.Session.GetInt32("userId");

            if (userId == null)
            {
                return RedirectToAction("Profile", "User");
            }

            var orders = _context.Orders
                                 .Where(o => o.UsersId == userId)
                                 .OrderByDescending(o => o.Date)
                                 .ToList();

            return View(orders);
        }

        public IActionResult OrderDetails(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            var orderItems = _context.OrderItems
                                     .Include(oi => oi.Product)
                                     .Where(oi => oi.OrderId == id)
                                     .ToList();

            var result = (order, orderItems); // استخدام Tuple

            return View(result);
        }


        //checout 

        [HttpGet]
        public IActionResult Checkout()
        {
            var userId = HttpContext.Session.GetInt32("userId");

            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }


           

            return View(user);
        }



        [HttpPost]
        public IActionResult Checkout(string paymentMethod , string Address , string Phone)
        {
            var userId = HttpContext.Session.GetInt32("userId");

            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (string.IsNullOrEmpty(paymentMethod))
            {
                ModelState.AddModelError(string.Empty, "Please select a payment method.");
                var user = _context.Users.FirstOrDefault(u => u.Id == userId);
                return View(user);
            }

            // جلب السلة للمستخدم
            var cart = _context.Carts.FirstOrDefault(c => c.UsersId == userId);
            if (cart == null)
            {
                ModelState.AddModelError(string.Empty, "Your cart is empty.");
                return View();
            }

            var cartItems = _context.CartItems
                .Where(ci => ci.CartId == cart.Id)
                .ToList();

            if (!cartItems.Any())
            {
                ModelState.AddModelError(string.Empty, "Your cart is empty.");
                return View();
            }

            // 1. إنشاء الدفع
            var payment = new Payment
            {
                CreateAt = DateTime.Now,
                Status = "Pending",
                PaymentMethoud = paymentMethod,
                Total = cart.TotalPrice
            };

            _context.Payments.Add(payment);
            _context.SaveChanges(); // عشان ناخذ الـ ID

            // 2. إنشاء الطلب وربطه بالمستخدم والدفع
            var order = new Order 
            {
                Quantity=cart.Quantity,
                UsersId = userId.Value,
                PaymentId = payment.Id,
                Date = DateTime.Now,
                //Status = "Processing",
                TotalAmount = cart.TotalPrice,
                Address = Address,
                Phone = Phone,
            };

            _context.Orders.Add(order);
            _context.SaveChanges(); // لحفظ order.Id

            // 3. إنشاء OrderItems من CartItems
            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Size = item.Size,
                    Price = item.Price,
                    TotalPrice = item.TotalPrice
                };

                _context.OrderItems.Add(orderItem);
            }

            // 4. تفريغ السلة
            _context.CartItems.RemoveRange(cartItems);
            cart.Quantity = 0;
            cart.TotalPrice = 0;
            _context.Carts.Update(cart);
            _context.SaveChanges();

            return RedirectToAction("Index","Home");
        }


    }

}
