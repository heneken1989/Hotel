using Hotel.Data;
using Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers
{
    [Route("SingleRoom/{action}")]
    [AllowAnonymous]
    public class SingleRoomController : Controller
    {
        HotelDbContext ctx;
        public SingleRoomController(HotelDbContext ctx)
        {
            this.ctx = ctx; 
        }
        public async Task<IActionResult> Index(int id)
        {
            var room = await ctx.Rooms
                .Include(a => a.Unities)
                .Include(b => b.roomProperties)
                .Include(c => c.Images)
                .Include(d => d.Details)
                .Include(e => e.RoomType)
                .SingleOrDefaultAsync(x => x.Id == id);
            var roomProperties = await ctx.RoomProperties.ToListAsync(); 
           ViewBag.RoomProperties = new SelectList(roomProperties, "Id", "Name");

            return View(room);
        }
    }
}
