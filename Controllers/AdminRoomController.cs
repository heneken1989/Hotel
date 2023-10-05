using Hotel.Data;
using Hotel.Models;
using Hotel.Models.Shared;
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

        HotelDbContext ctx;

        public AdminRoomController(HotelDbContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<IActionResult> Index()
        {
            var rooms = await ctx.Rooms
                .Include(r => r.Unities)
                .Include(r => r.RoomType)
                .ToListAsync();


            var roomProperties = await ctx.RoomProperties
                .Include(a => a.Details)
                .ToListAsync();

            ViewBag.RoomPropertyList = new SelectList(roomProperties, "Id", "Name");

            return View(rooms);
        }
        public async Task<IActionResult> Create()
        {
            var room = new Room
            {
                Unities = ctx.RoomUnities
                    .Where(a => a.RoomId == null)
                    .ToList(),

                roomProperties = ctx.RoomProperties.ToList()

            };
            ViewBag.RoomTypeId = new SelectList(ctx.RoomTypes, "Id", "Type");

            return View(room);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Room ro, List<int> Unities, List<IFormFile> ImageFile, List<string> PropertyDetail, List<int> PropertyId)
        {

            if (ModelState.IsValid)
            {
                // add new room
                ctx.Rooms.Add(ro);
                await ctx.SaveChangesAsync();


                // add Property Detail
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
                foreach (var imgfile in ImageFile)

                {

                    if (imgfile != null)
                    {
                        var FileName = await CommonMethod.uploadImage(imgfile);
                        if (FileName != "false")
                        {
                            var newImage = new Image
                            {
                                Url = filename,
                                RoomId = ro.Id
                            };
                            ctx.Images.Add(newImage);
                            await ctx.SaveChangesAsync();
                        }
                        else
                        {
                            return View();
                        }
                    }
                }

                //LIst danh sach Unity Selected
                var selectedUnity = await ctx.RoomUnities
                     .Where(u => Unities.Contains(u.Id))
                     .ToListAsync();
                // Tao New Unity have RoomID = Created Room (ro)
                foreach (var u in selectedUnity)
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


        public async Task<IActionResult> Edit(int id)
        {
            var room = await ctx.Rooms
                .Include(a => a.Unities)
                .Include(b => b.RoomType)
                .SingleOrDefaultAsync(a => a.Id == id);

            var roomProperties = await ctx.RoomProperties
             .Include(a => a.Details)
             .ToListAsync();

            ViewBag.RoomPropertyList = new SelectList(roomProperties, "Id", "Name");

            var propertyDetails = await ctx.RoomPropertyDetails
                   .Where(d => d.RoomId == id)
                   .ToListAsync();

            ViewBag.PropertyDetails = propertyDetails;
            ViewBag.RoomTypeList = new SelectList(ctx.RoomTypes.ToList(), "Id", "Type");
            ViewBag.RoomUnitiesList = new SelectList(ctx.RoomUnities.Where(a => a.RoomId == null).ToList(), "Id", "Name");
            return View(room);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Room ro, List<int> Unities, List<IFormFile> ImageFile, List<string> PropertyDetail, List<int> PropertyId)
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 08290b26c108c0631db74c2cd4be3145b4a3974d

        {
            if (ModelState.IsValid)
            {
                //adit
                ctx.Entry(ro).State = EntityState.Modified;
<<<<<<< HEAD
=======
=======
        
        {
            if(ModelState.IsValid)
            {
                //adit
                ctx.Entry(ro).State = EntityState.Modified; 
>>>>>>> 452dde47dc47efcdc9f68a082bd9b26c03263b3c
>>>>>>> 08290b26c108c0631db74c2cd4be3145b4a3974d
                await ctx.SaveChangesAsync();


                // Edit Property Detail
                for (int i = 0; i < PropertyDetail.Count; i++)
                {
                    var Prodetail = await ctx.RoomPropertyDetails
                        .Where(a => a.RoomId == ro.Id)
                        .Where(a => a.RoomPropertyId == PropertyId[i])
                        .SingleOrDefaultAsync();

                    Prodetail.Detail = PropertyDetail[i];
<<<<<<< HEAD

=======
<<<<<<< HEAD

=======
                
>>>>>>> 452dde47dc47efcdc9f68a082bd9b26c03263b3c
>>>>>>> 08290b26c108c0631db74c2cd4be3145b4a3974d
                    Console.WriteLine($"idddddddddddddddddd:{Prodetail.RoomId}");
                    ctx.Entry(Prodetail).State = EntityState.Modified;
                    await ctx.SaveChangesAsync();
                }


                // Edit Images

                string filename = string.Empty;
                foreach (var imgfile in ImageFile)
                {

                    if (imgfile != null)
                    {
                        var FileName = await CommonMethod.uploadImage(imgfile);
                        if (FileName != "false")
                        {
                            var newImage = new Image
                            {
                                Url = filename,
                                RoomId = ro.Id
                            };
                            ctx.Images.Add(newImage);
                            await ctx.SaveChangesAsync();
                        }
                        else
                        {
                            return View();
                        }
                    }
                }

                var oldUnity = await ctx.RoomUnities
                   .Where(a => a.RoomId == ro.Id)
                   .ToListAsync();

<<<<<<< HEAD
                for (int i = 0; i < oldUnity.Count; i++)
=======
<<<<<<< HEAD
                for (int i = 0; i < oldUnity.Count; i++)
=======
                for(int i=0;i<oldUnity.Count;i++)
>>>>>>> 452dde47dc47efcdc9f68a082bd9b26c03263b3c
>>>>>>> 08290b26c108c0631db74c2cd4be3145b4a3974d
                {
                    ctx.Entry(oldUnity[i]).State = EntityState.Deleted;
                    await ctx.SaveChangesAsync();

                }
<<<<<<< HEAD

=======
<<<<<<< HEAD

=======
            
>>>>>>> 452dde47dc47efcdc9f68a082bd9b26c03263b3c
>>>>>>> 08290b26c108c0631db74c2cd4be3145b4a3974d
                // Edit Unity
                //LIst danh sach Unity Selected
                var selectedUnity = await ctx.RoomUnities
                     .Where(u => Unities.Contains(u.Id))
                     .ToListAsync();
                // Tao New Unity have RoomID = Created Room (ro)
                foreach (var u in selectedUnity)
                {
<<<<<<< HEAD

=======
<<<<<<< HEAD

=======
           
>>>>>>> 452dde47dc47efcdc9f68a082bd9b26c03263b3c
>>>>>>> 08290b26c108c0631db74c2cd4be3145b4a3974d

                    var newUnity = new RoomUnity
                    {
                        Name = u.Name,
                        RoomId = ro.Id
                    };
<<<<<<< HEAD
                    ctx.Entry(newUnity).State = EntityState.Added;
=======
<<<<<<< HEAD
                    ctx.Entry(newUnity).State = EntityState.Added;
=======
                    ctx.Entry(newUnity).State = EntityState.Added;   
>>>>>>> 452dde47dc47efcdc9f68a082bd9b26c03263b3c
>>>>>>> 08290b26c108c0631db74c2cd4be3145b4a3974d
                }
                await ctx.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(ro);

        }




    }
}
