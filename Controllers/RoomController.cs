using Hotel.Data;
using Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers
{
    [AllowAnonymous]
    public class RoomController : Controller
    {
        HotelDbContext ctx;
        public RoomController(HotelDbContext ctx)
        {
            this.ctx = ctx; 
        }

        public async Task<IActionResult> Index()
        {
          
            var rooms = await ctx.Rooms

                .Include(a => a.RoomType)
                .Include(a => a.Images!.Take(1))
            
                .ToListAsync();


            var RoomPro = await ctx.RoomProperties

                 .Include(a => a.Details)
                 .ToListAsync();


            ViewBag.RoomList = new SelectList(rooms, "Id", "Name");
            ViewBag.RoomProList = new SelectList(RoomPro, "Id", "Name");
			return View();

        }

    }
}
