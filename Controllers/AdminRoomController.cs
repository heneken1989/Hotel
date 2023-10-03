using Hotel.Data;
using Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers
{
    [Route("admin/Room/{action}")]
    [AllowAnonymous]
    public class AdminRoomController : Controller
    {
        IWebHostEnvironment env;
        HotelDbContext ctx;

        public AdminRoomController(HotelDbContext ctx, IWebHostEnvironment env)
        {
            this.ctx = ctx;
            this.env = env;
        }
        public async Task<IActionResult> Index()
        {
            var rooms = await ctx.Rooms
                .Include(r=>r.Unities)
                .Include(r=>r.RoomType)
                .ToListAsync();
         
            return View(rooms);
        }
        public async Task<IActionResult> Create() 
        {
            var room = new Room
            {
                Unities = ctx.RoomUnities
                    .Where(a=>a.RoomId== null)
                    .ToList(),

                roomProperties = ctx.RoomProperties.ToList()
          
            };
            ViewBag.RoomTypeId = new SelectList(ctx.RoomTypes, "Id", "Type");
      
            return View(room);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Room ro,List<int> Unities, List<IFormFile> ImageFile,List<string> PropertyDetail ,List<int> PropertyId)
        {
       
            if (ModelState.IsValid) 
            {
                // add new room
                ctx.Rooms.Add(ro);
                await ctx.SaveChangesAsync();



                for (int i = 0; i < PropertyDetail.Count; i++)
                {
                    var newPropertyDetail = new RoomPropertyDetail
                    {
                        RoomPropertyId = PropertyId[i],
                        Detail = PropertyDetail[i],
                        RoomId = ro.Id
                    };
                    ctx.RoomPropertyDetails.Add(newPropertyDetail);
                   
                }
                await ctx.SaveChangesAsync();

                string filename = string.Empty;
                // kiem tra xem hinh co duoc upload len hay khong
                foreach(var imgfile in ImageFile) 
                
                {

                    if (imgfile != null)
                    {
                        filename = imgfile.FileName;
                        var imagesFolder = Path.Combine(env.WebRootPath, "images");
                        // kiem tra xem thu muc wwwroot/images da co hay chua
                        if (!Directory.Exists(imagesFolder))
                        {
                            // neu chua co tao moi
                            Directory.CreateDirectory(imagesFolder);
                        }
                        var filepath = Path.Combine(imagesFolder, filename);

                        using (var stream = new FileStream(filepath, FileMode.Create))
                        {
                            await imgfile.CopyToAsync(stream);
                        }

                        var newImage = new Image
                        {
                            Url = filename,
                            RoomId = ro.Id
                        };
                        ctx.Images.Add(newImage);
                        await ctx.SaveChangesAsync();
                    }
                }
              
                //LIst danh sach Unity Selected
                var selectedUnity = await ctx.RoomUnities
                     .Where(u=>Unities.Contains(u.Id))
                     .ToListAsync();   
                // Tao New Unity have RoomID = Created Room (ro)
                foreach(var u in selectedUnity)
                {
                    var newUnity = new RoomUnity
                    {
                        Name = u.Name,
                        RoomId = ro.Id
                    };
                    ctx.RoomUnities.Add(newUnity);
                }
                await ctx.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ro);
        }


    }
}
