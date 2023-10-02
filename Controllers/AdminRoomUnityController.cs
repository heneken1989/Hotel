using Hotel.Data;
using Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers
{
    [Route("admin/RoomUnity/{action}")]
    [AllowAnonymous]
    public class AdminRoomUnityController : Controller
    {
        HotelDbContext ctx;
        public AdminRoomUnityController(HotelDbContext ctx)
        {
            this.ctx = ctx; 
        }
		public async Task<IActionResult> Index()
		{
			var types = await ctx.RoomUnities.ToListAsync();

			return View(types);
		}

		public IActionResult Create()
		{

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(RoomUnity unity)
		{
			if (ModelState.IsValid)
			{
				ctx.Entry<RoomUnity>(unity).State = EntityState.Added;
				await ctx.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			else
			{
				return View("Create", unity);

			}
		}
	}
}
