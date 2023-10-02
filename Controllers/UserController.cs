using Hotel.Data;
using Hotel.Dtos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Hotel.Controllers
{
<<<<<<< HEAD
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
					.Where(u => u.Username == data.Email && u.Password == data.Password)
					.FirstOrDefaultAsync();

				if (check == null)
				{
					ViewData["Error"] = "Tên đăng nhập hoặc mật khẩu không đúng vui lòng kiểm tra lại";
					return View("Login");

				}
				else
				{
					var claims = new List<Claim>()
					{

						new Claim(ClaimTypes.NameIdentifier,check.Username
					
						)
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

=======
    public class UserController : Controller
    {
        private readonly HotelDbContext _context;

        public UserController(HotelDbContext context) {
        _context=context;   
        }
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View("Login");
        }

        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login( LoginDto data)
        {
            if (ModelState.IsValid)
            {
          
                var check= await _context.Users
                    .Where(u=>u.Username==data.Email && u.Password==data.Password)
                    .FirstOrDefaultAsync();

                if (check == null)
                {
                    ViewData["Error"] = "Tên đăng nhập hoặc mật khẩu không đúng vui lòng kiểm tra lại";
                    return View("Login");

                }
                else
                {
                    var claims = new List<Claim>()
                    {
                       
                        new Claim(ClaimTypes.NameIdentifier,check.Username)
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
			
>>>>>>> e3581440741119de798def142fb39ed350b68525
		}

		[Route("unauthozied")]
		public IActionResult UnAuthorize()
		{
			return View("unauthorize");
		}
	}
}
<<<<<<< HEAD

=======
>>>>>>> e3581440741119de798def142fb39ed350b68525
