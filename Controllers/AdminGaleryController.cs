using Hotel.Data;
using Hotel.Models;
using Hotel.Models.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers
{
	[Route("admin/image/{action}")]
	public class AdminGaleryController : Controller
    {
        private readonly HotelDbContext _context;
        public AdminGaleryController(HotelDbContext context) {
        _context=context;
        }
        public async Task <IActionResult> Index()
        {

            string? success = TempData["success"] as string;

            if (success != null)
            {
                ViewData["ChangePassSuccessed"] = success;
            }

            string? error = TempData["error"] as string;

            if (error != null)
            {
                ViewData["ChangePassSuccessed"] = error;
            }
            var data=await _context.Galleries.ToListAsync();
            return View(data);
        }

        public IActionResult Create ()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(List<IFormFile> image)
        {
            foreach ( var img in image)
            {
				var result = await CommonMethod.uploadImage(img);
				if (result == "false")
				{
					ViewData["error"] = "Có lỗi xảy ra vui lòng thử lại sau";
					return View("Create");
				}
				else
				{
					await _context.AddAsync(new Gallery() { Url = result });
					await _context.SaveChangesAsync();
					
				}
			}
			TempData["success"] = "Ảnh đã  đã được thêm thành công";
			return RedirectToAction("Index");


		}

        public async Task<IActionResult> Delete(int id)
        {
            var check = await _context.Galleries.FindAsync(id);
            if (check == null)
            {
                TempData["error"] = "Có lỗi xảy ra vui lòng thử lại sau";
                return RedirectToAction("Index");
            }
            else
            {
                _context.Entry(check).State = EntityState.Deleted;
               await _context.SaveChangesAsync();
                TempData["success"] = "Ảnh đã  đã được xóa thành công";
                return RedirectToAction("Index");
            }

        }
    }
}
