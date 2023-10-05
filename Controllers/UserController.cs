using Hotel.Data;
using Hotel.Dtos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.CodeAnalysis.Scripting;
using Hotel.Models;

namespace Hotel.Controllers
{
    public class UserController : Controller
    {
        private readonly HotelDbContext _context;

        public UserController(HotelDbContext context)
        {
            _context = context;
        }
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            string message = TempData["passchange"] as string;
            if (message != null)
            {
                ViewData["ChangePassSuccessed"] = message;
            }

            return View("Login");
        }

        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto data)
        {
            if (ModelState.IsValid)
            {

                var check = await _context.Users
                    .Where(u => u.Username == data.Email)
                    .FirstOrDefaultAsync();

                if (check == null)
                {
                    ViewData["Error"] = "Tên đăng nhập hoặc mật khẩu không đúng vui lòng kiểm tra lại";
                    return View("Login");

                }
                else
                {

                    if (!BCrypt.Net.BCrypt.Verify(data.Password, check.Password))
                    {
                        ViewData["Error"] = "Tên đăng nhập hoặc mật khẩu không đúng vui lòng kiểm tra lại";
                        return View("Login");
                    }

                    var claims = new List<Claim>()
                    {

                        new Claim(ClaimTypes.NameIdentifier,check.Username),
                         new Claim("id", check.Id.ToString())
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = true
                    };
                    await HttpContext
                        .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                    return RedirectToAction("Index", "Home");
                }

            }

            return View("Login");
        }




        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");

        }

        [Route("changePassword")]
        [HttpGet]

        public IActionResult ChangePassword()
        {
            return View("ChangePassword");
        }


        [Route("Register")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View("Register");
        }

        [Route("Register")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(string name, string pass)
        {
            var data = new User()
            {


                Username = name,

                Password = BCrypt.Net.BCrypt.HashPassword(pass)
            };
            await _context.AddAsync(data);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login");

        }

        [Route("changePassword")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(PassDto data)
        {
            if (ModelState.IsValid)
            {
                var userIdClaim = User.FindFirst("id");
                if (userIdClaim != null)
                {
                    string userId = userIdClaim.Value;
                    var check = await _context.Users.FindAsync(int.Parse(userId));
                    if (check == null)
                    {
                        ViewData["Error"] = "Tài khoản hiện tại không tồn tại";
                        return View();
                    }
                    else
                    {
                        if (!BCrypt.Net.BCrypt.Verify(data.CurrentPassword, check.Password))
                        {
                            ViewData["Error"] = "Mật khẩu hiện tại sai, vui lòng kiểm tra lại";
                            return View();
                        }
                        else
                        {
                            var newPassword = BCrypt.Net.BCrypt.HashPassword(data.NewPassword);
                            check.Password = newPassword;
                            await _context.SaveChangesAsync();
                            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                            TempData["passchange"] = "Mật khẩu đã thay đổi thành công, vui đăng nhập lại";
                            return RedirectToAction("Login");
                        }
                    }

                }
                return View("ChangePassword");
            }
            return View("ChangePassword");
        }





        [Route("unauthozied")]
        public IActionResult UnAuthorize()
        {
            return View("unauthorize");
        }
    }
}