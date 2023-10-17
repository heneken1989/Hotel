using Hotel.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers
{
    [AllowAnonymous]
    public class AboutController : Controller
    {
        private readonly HotelDbContext _context;

        public AboutController(HotelDbContext context) {
        _context=context;
        }
        public async  Task<IActionResult> Index()
        {
            var data =await _context.Galleries.ToListAsync();
            return View(data);
        }
    }
}
