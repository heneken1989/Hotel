using Hotel.Data;
using Hotel.Dtos;
using Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers
{
    [Route("SingleRoom/{action}")]
    [AllowAnonymous]
    public class SingleRoomController : Controller
    {
        HotelDbContext ctx;
        public SingleRoomController(HotelDbContext ctx)
        {
            this.ctx = ctx; 
        }
        public async Task<IActionResult> Index(int id)
        {
            string? message = TempData["success"] as string;
            if (message != null)
            {
                ViewData["success"] = message;
            }


            string? error = TempData["error"] as string;
            if (error != null)
            {
                ViewData["error"] = error;
            }
            var room = await ctx.Rooms
                .Include(a => a.Unities)
                .Include(b => b.roomProperties)
                .Include(c => c.Images)
                .Include(d => d.Details)
                .Include(e => e.RoomType)
                .SingleOrDefaultAsync(x => x.Id == id);
            var roomProperties = await ctx.RoomProperties.ToListAsync(); 
           ViewBag.RoomProperties = new SelectList(roomProperties, "Id", "Name");

            return View(new RoomDto()
            {
                Room=room
            });
        }

        public async Task<IActionResult> HandleAddOrder(RoomDto data,int id) 
        {
         
          
            if (ModelState.IsValid)
            {
                AdminOrderController action = new AdminOrderController(ctx);
                var result =await action.Add(data.Order);
                if (result == true)
                {
                    TempData["success"] = "Thông tin đã được gửi đi thành công. Nhân viên sẽ liên hệ để xác nhận thông tin sớm nhất có thể.Xin cảm ơn !";
                    return RedirectToAction("Index", new { id = id });
                }
                else
                {
                    TempData["error"] = "Có lỗi xảy ra vui lòng. nhập lại sau";
                    return RedirectToAction("Index", new { id=id});
                }

            }
            TempData["error"] = "Vui lòng nhập đủ tên và địa chỉ";
            return RedirectToAction("Index", new { id = id });

        }
    }
}
