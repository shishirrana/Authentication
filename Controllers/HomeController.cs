using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using reviseAuthentication.Data;
using reviseAuthentication.Models;
using System.Diagnostics;
using reviseAuthentication.Controllers.VM;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace reviseAuthentication.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _context;

        public HomeController(ILogger<HomeController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        //AccessDenied Page

        public IActionResult AccessDenied()
        {
            return View();
        }


        [AllowAnonymous]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Signup(SignupVM user)
        {
            if(ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                TempData["Signup"] = "Account created successfully";
                return RedirectToAction("Login");

            }
            else
            {
                TempData["SignupError"] = "Failed to Create Account";
            }
            return View();
        }

       
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var data = _context.Users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();

                if (data != null)
                {
                    addingClaimIdentity(user);
                    TempData["Login"] = "Logged In successfully";
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["LoginError"] = "Login Failed";
                    ModelState.AddModelError("Password", "Invalid Username or Passsword");

                }


            }
            return View(user);
        }

        private void addingClaimIdentity(User user)
        {

            var userClaims = new List<Claim>()
            {

                new Claim("UserName", user.Email),
                new Claim(ClaimTypes.Role,"admin")            

            };

            var claimsIdentity = new ClaimsIdentity(
                userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

            //httpcontext, current user is authentic user
            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
                );
        }

        public IActionResult Signout() 
        {
            HttpContext.SignOutAsync();
            TempData["Signout"] = "Logged Out successfully";
            return RedirectToAction("Login");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}