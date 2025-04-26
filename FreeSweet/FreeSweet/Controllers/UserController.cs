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
            return View();
        }

        //public IActionResult Cart() 
        //{
        //    return View();
        //}


        //public IActionResult Checkout()
        //{
        //    return View();
        //}
       
    }

}
