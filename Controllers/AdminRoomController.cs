using Hotel.Data;
using Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers
{
    [Route("admin/Room/{action}")]
    [AllowAnonymous]
    public class AdminRoomController : Controller
    {
        HotelDbContext ctx;

        public AdminRoomController(HotelDbContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<IActionResult> Index()
        {
            var rooms = await ctx.Rooms
                .Include(r=>r.Unities)
                .Include(r=>r.RoomType)
                .ToListAsync();
         
            return View(rooms);
        }
        public async Task<IActionResult> Create() 
        {
            var room = new Room
            {
                Unities = ctx.RoomUnities
                    .Where(a=>a.RoomId== null)
                    .ToList()
            };


            ViewBag.RoomTypeId = new SelectList(ctx.RoomTypes, "Id", "Type");

            return View(room);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Room ro,List<int> Unities)
        {
            if(ModelState.IsValid) 
            {
                // add new room
                ctx.Rooms.Add(ro);
                await ctx.SaveChangesAsync();

                //LIst danh sach Unity Selected
                var selectedUnity = await ctx.RoomUnities
                     .Where(u=>Unities.Contains(u.Id))
                     .ToListAsync();   
                // Tao New Unity have RoomID = Created Room (ro)
                foreach(var u in selectedUnity)
                {
                    var newUnity = new RoomUnity
                    {
                        Name = u.Name,
                        RoomId = ro.Id
                    };
                    ctx.RoomUnities.Add(newUnity);
                }
                await ctx.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ro);
        }


    }
}
