using Hotel.Data;
using Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Hotel.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HotelDbContext _context;

        public HomeController(ILogger<HomeController> logger, HotelDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var banner = await _context.Banners.ToListAsync();
            var rooms = await _context.Rooms

                .Include(a => a.RoomType)
                .Include(a => a.Images!.Take(1))
                .ToListAsync();


            var roomProperties = await _context.RoomProperties
                 .Where(a => a.Name == "Giá Phòng")
                 .Include(a => a.Details)
                 .SingleOrDefaultAsync();


            ViewBag.RoomList = new SelectList(rooms, "Id", "Name");
            ViewBag.RoomPropertyList = roomProperties;
            return View(banner);

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