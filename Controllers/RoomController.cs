using Hotel.Data;
using Hotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers
{
 
    public class RoomController : Controller
    {
        HotelDbContext ctx;
        public RoomController(HotelDbContext ctx)
        {
            this.ctx = ctx; 
        }
     
        public async Task<IActionResult> Index()
        {
        

            return View();
        }

    }
}
