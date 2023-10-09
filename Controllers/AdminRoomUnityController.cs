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

		public async Task<IActionResult> Delete(int id)
		{
			var uni = await ctx.RoomUnities.SingleOrDefaultAsync(a=>a.Id ==id);
			ctx.Entry(uni!).State = EntityState.Deleted;
			await ctx.SaveChangesAsync();	
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Edit(int id)
		{

			var uni = await ctx.RoomUnities.SingleOrDefaultAsync(a=>a.Id ==id);
			return View(uni);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(RoomUnity unity)
		{
			if (ModelState.IsValid)
            {
                ctx.Entry(unity).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
                return Redirect("Index");

            }
			else
			{
				var U = await ctx.RoomUnities.AsNoTracking().SingleOrDefaultAsync(a => a.Id == unity.Id);
				if (unity.Name == U.Name)
				{
					return Redirect("Index");

				}
				return View(unity);
			}



		}
	}
}
