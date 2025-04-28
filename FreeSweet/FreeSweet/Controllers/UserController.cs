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

            TempData["message"] = "User added!";

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
            return View(user);
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


        //[HttpPost]
        //public IActionResult ResetPassword(string currentPassword, string newPassword, string confirmPassword)
        //{
        //    if (newPassword != confirmPassword)
        //    {
        //        ViewBag.ErrorMessage = "Passwords do not match.";
        //        return View();
        //    }
        //    string userEmail = HttpContext.Session.GetString("UserEmail");
        //    var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
        //    if (currentPassword != user.Password)
        //    {
        //        ViewBag.ErrorMessage = "Current Passwords do not match.";
        //        return View();
        //    }

        //        if (!user.Password.StartsWith("$2a$"))
        //    {

        //        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        //        _context.SaveChanges();
        //    }
        //    if (user != null)
        //    {

        //        if (BCrypt.Net.BCrypt.Verify(currentPassword, user.Password))
        //        {

        //            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);


        //            user.Password = hashedPassword;
        //            _context.SaveChanges();
        //            return RedirectToAction("Profile");
        //        }
        //        else
        //        {

        //            ViewBag.ErrorMessage = "Incorrect old password.";
        //            return View();
        //        }
        //    }
        //    else
        //    {

        //        ViewBag.ErrorMessage = "User not found.";
        //        return View();
        //    }

        //}

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

    


        //public IActionResult Checkout()
        //{
        //    // جلب userId من الـSession
        //    var userId = HttpContext.Session.GetInt32("userId");

        //    if (userId == null)
        //    {
        //        // توجيه المستخدم إلى صفحة تسجيل الدخول إذا لم يكن في الجلسة
        //        return RedirectToAction("Login", "User");
        //    }

        //    // استرجاع بيانات المستخدم من قاعدة البيانات باستخدام الـuserId
        //    var user = _context.Users.FirstOrDefault(u => u.Id == userId);

        //    if (user == null)
        //    {
        //        // إذا لم يتم العثور على المستخدم
        //        return NotFound();
        //    }

        //    // تمرير بيانات المستخدم إلى الـView
        //    return View(user);
        //}

        //[HttpPost]
        //public IActionResult Payment(Payment payment)
        //{
        //    // التأكد من أن البيانات المدخلة غير فارغة
        //    if (ModelState.IsValid)
        //    {
        //        // تعيين القيمة للمستخدم المبدئي إذا لزم الأمر (مثل استخدام الـ Session أو الـ UserId)
        //        var userId = HttpContext.Session.GetInt32("userId");

        //        if (userId != null)
        //        {
        //            payment.CreateAt = DateTime.Now;
        //            payment.Status = "Pending";  // حالة الدفع (مثلاً: Pending أو Completed)
        //            payment.PaymentMethoud = Request.Form["paymentMethod"]; // أو عبر ID القيم الخاصة بالطرق
        //            payment.Total = 100; // تعيين القيمة الإجمالية بناءً على عملية الشراء

        //            _context.Payments.Add(payment);  // إضافة الدفع إلى جدول الـPayments
        //            _context.SaveChanges();  // حفظ التغييرات في قاعدة البيانات
        //        }

        //        // تحويل المستخدم إلى صفحة تأكيد الدفع أو صفحة أخرى
        //        return RedirectToAction("Confirmation");
        //    }

        //    // في حالة وجود أخطاء في النموذج (مثل الحقول الفارغة)
        //    return View(payment);
        //}

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
        public IActionResult Checkout(string paymentMethod)
        {
            var userId = HttpContext.Session.GetInt32("userId");

            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (string.IsNullOrEmpty(paymentMethod))
            {
                // لو المستخدم ما اختار وسيلة دفع
                ModelState.AddModelError(string.Empty, "Please select a payment method.");
                var user = _context.Users.FirstOrDefault(u => u.Id == userId);
                return View(user);
            }

            var payment = new Payment
            {
                //UserId = (int)userId,
                CreateAt = DateTime.Now,
                Status = "Pending",
                PaymentMethoud = paymentMethod, // جلب الطريقة من الفورم
                Total = 100 // مثال: حط المبلغ الحقيقي تبع السلة هنا
            };

            _context.Payments.Add(payment);
            _context.SaveChanges();

            return RedirectToAction("Confirmation");
        }



    }

}
