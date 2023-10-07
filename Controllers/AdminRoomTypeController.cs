using Hotel.Data;
using Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers

{
    [Route("admin/RoomType/{action}")]
    [AllowAnonymous]
   
    public class AdminRoomTypeController : Controller
    {
        HotelDbContext ctx;
        public AdminRoomTypeController(HotelDbContext ctx)
        {
            this.ctx = ctx; 
        }
       

        public async Task<IActionResult> Index()
        {
            var types = await ctx.RoomTypes.ToListAsync();

            return View(types);
        }
      
        public  IActionResult Create()
        {

            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoomType room)
        {
            if(ModelState.IsValid) 
            {
                ctx.Entry<RoomType>(room).State = EntityState.Added;
                await ctx.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View("Create", room);

            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            var type = await ctx.RoomTypes.SingleOrDefaultAsync(a => a.Id == id);
            var room = await ctx.Rooms
                .Where(r=>r.RoomType.Id == id)
                .ToListAsync();
            if(room.Count==0)
            {
                ctx.Entry(type!).State = EntityState.Deleted;
                await ctx.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Không thể xóa vì có tồn tại phòng trong Loại Phòng này!";
                return RedirectToAction("Index");
            }
          
        }

        public async Task<IActionResult> Edit(int id)
        {
            var type = await ctx.RoomTypes.SingleOrDefaultAsync(a => a.Id == id);
            return View(type);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoomType Rtype)
        {
            ctx.Entry(Rtype).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
            return Redirect("Index");
        }

    }
}
