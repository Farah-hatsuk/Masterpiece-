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
        //public IActionResult Registration()
        //{
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(User user)
        {
            //user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }



        //public IActionResult Login()
        //{
        //    return View();
        //}

        public IActionResult Login(User user)
        {
            var userinfo = _context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);

            if (userinfo != null)
            {

                HttpContext.Session.SetString("UserName", userinfo.Name);
                HttpContext.Session.SetString("UserEmail", userinfo.Email);
                HttpContext.Session.SetString("UserImg", userinfo.Img);
                HttpContext.Session.SetString("UserPhone", userinfo.Phone);
                HttpContext.Session.SetString("UserAddress", userinfo.Address);
                HttpContext.Session.SetString("UserPassword", userinfo.Password);

                if (userinfo.Role == "Admin")
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                return RedirectToAction("Index", "Admin");


            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }


        // profile 

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult EditProfile()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return View(user);
        }

        public IActionResult EditProfile(User UpdatUser , IFormFile profileImage)
        {
            var email = HttpContext.Session.GetString("UserEmail");
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null) { 
            
                user.Name= UpdatUser.Name;
                user.Email=UpdatUser.Email;
                user.Phone=UpdatUser.Phone;
                user.Address=UpdatUser.Address;

                if (profileImage != null) {
                    string fileName = Path.GetFileName(profileImage.FileName);
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/User_image", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        profileImage.CopyTo(stream);
                    }
                    UpdatUser.Img = "/User_image/" + fileName;

                    _context.SaveChanges();
                    HttpContext.Session.SetString("UserName", user.Name);
                    HttpContext.Session.SetString("UserImg", user.Img);
                    HttpContext.Session.SetString("UserPhone", user.Phone);
                    HttpContext.Session.SetString("UserAddress", user.Address);
                    return RedirectToAction("Profile");
                }
            }
         
            return View(user);
        }





        public IActionResult HistoryOfOrder()
        {
            return View();
        }

        public IActionResult Cart() 
        {
            return View();
        }


        public IActionResult Checkout()
        {
            return View();
        }
    }

}
