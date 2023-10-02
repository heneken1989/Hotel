using Hotel.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers
{
    public class AdminRoomUnityController : Controller
    {
        HotelDbContext ctx;
        public AdminRoomUnityController(HotelDbContext ctx)
        {
            this.ctx = ctx; 
        }
        public async Task<IActionResult> Index()
        {
            var ListUnity = await ctx.RoomUnities.ToListAsync();
            return View(ListUnity);
        }
    }
}
