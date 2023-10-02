using Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hotel.Controllers
{
<<<<<<< HEAD
	[AllowAnonymous]
	public class HomeController : Controller
=======
    [AllowAnonymous]
    public class HomeController : Controller
>>>>>>> e3581440741119de798def142fb39ed350b68525
    {
       

        public HomeController()
        {
     
        }
      
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}