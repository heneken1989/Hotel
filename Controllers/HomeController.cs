using Hotel.Data;
using Hotel.Dtos;
using Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
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
                .Include(r => r.RoomType)
                .Include(r => r.Images)
                .Include(r => r.Details)
                .ToListAsync();

            foreach (var data in rooms)
            {
                foreach (var detail in data.Details)
                {
                    var s = await _context.RoomProperties.FindAsync(detail.RoomPropertyId);
                    detail.Name = s.Name;
                }
            }


            return View(new HomeDto()
            {
                Banners = banner,
                Rooms = rooms,
            });

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