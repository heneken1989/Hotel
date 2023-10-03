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
				return RedirectToAction("Index");
			}
            return View(pro);   


  
        }

    }
}
