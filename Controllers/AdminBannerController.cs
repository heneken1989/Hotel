using Hotel.Data;
using Hotel.Dtos;
using Hotel.Models;
using Hotel.Models.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers
{
    [Route("admin/banner/{action}")]
    [AllowAnonymous]
    public class AdminBannerController : Controller
    {
        private readonly HotelDbContext _context;

        public AdminBannerController(HotelDbContext context) { 
        _context=context;
        }
        public async Task<IActionResult> Index()
        {
            var data =await _context.Banners.ToListAsync();

            return View(data);
        }

        public IActionResult Create() {
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(BannerDto data)
        {
            if(ModelState.IsValid)
            {
                var image =await CommonMethod.uploadImage(data.image);
                if (image!="false")
                {
                    await _context.AddAsync(new Banner(data, image));
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");   
                }
            }
            return View("Create");
            
        }
    }
}
