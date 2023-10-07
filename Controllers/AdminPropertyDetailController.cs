﻿using Hotel.Data;
using Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers
{
    [Route("admin/RoomPropertyDetail/{action}")]
    [AllowAnonymous]
    public class AdminPropertyDetailController : Controller
    {
        HotelDbContext ctx;
        public AdminPropertyDetailController(HotelDbContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<IActionResult> Index()
        {
            var prodetail = await ctx.RoomPropertyDetails.ToListAsync();

            return View(prodetail);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.RoomPropertyDetail = new SelectList(ctx.RoomProperties, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoomPropertyDetail Prodetail)
        {
            if(ModelState.IsValid) 
            {
                ctx.Entry(Prodetail).State = EntityState.Added;
                await ctx.SaveChangesAsync();   
                return RedirectToAction("Index");   
            }
            return View(Prodetail);    
        }
    }
}
