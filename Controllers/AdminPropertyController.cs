using Hotel.Data;
using Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers
{
    [Route("admin/RoomProperty/{action}")]
    [AllowAnonymous]
    public class AdminPropertyController : Controller
    {
        HotelDbContext ctx;
        public AdminPropertyController(HotelDbContext ctx)
        {
            this.ctx = ctx; 
        }
        
        public async Task<IActionResult> Index()
        {
            var properties =  await ctx.RoomProperties.ToListAsync();

            return View(properties);
        }
        public  IActionResult Create()
        {
            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoomProperty pro)
        {
            if(ModelState.IsValid)
            {
				ctx.Entry(pro).State = EntityState.Added;
				await ctx.SaveChangesAsync();
                var rooms = await ctx.Rooms.ToListAsync();
            
                foreach(var a in rooms)
                {
                    var prodetail = new RoomPropertyDetail
                    {
                        Detail = "",
                        RoomPropertyId = pro.Id,
                        RoomId = a.Id   
                    };
                    ctx.Entry(prodetail).State = EntityState.Added;
                    await ctx.SaveChangesAsync();
                }
				return RedirectToAction("Index");
			}
            return View(pro);   
        }

        public async Task<IActionResult> Delete(int id)
        {
            var pro = await ctx.RoomProperties.SingleOrDefaultAsync(a => a.Id == id);
            var details = await ctx.RoomPropertyDetails
                .Where(r => r.RoomPropertyId == id)
                .ToListAsync();
            if (details.Count == 0)
            {
                ctx.Entry(pro!).State = EntityState.Deleted;
                await ctx.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Không thể xóa vì có tồn tại Detail trong Loại Property này!";
                return RedirectToAction("Index");
            }

        }

        public async Task<IActionResult> Edit(int id)
        {
            var type = await ctx.RoomProperties.SingleOrDefaultAsync(a => a.Id == id);
            return View(type);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoomProperty Rtype)
        {
            ctx.Entry(Rtype).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
            return Redirect("Index");
        }


    }
}
