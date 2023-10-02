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

    }
}
