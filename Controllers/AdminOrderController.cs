using Hotel.Data;
using Hotel.Dtos;
using Hotel.Migrations;
using Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers
{
	[Route("admin/order/{action}")]
	[AllowAnonymous]
	public class AdminOrderController : Controller
    {
        private readonly HotelDbContext _context;
        public AdminOrderController(HotelDbContext context) { 
        _context=context;
        }
        public async Task <IActionResult> Index()
        {
            var data= await  _context.Orders
                .OrderByDescending(o=>o.CreatedDate)
                .ToListAsync();
            return View(data);
        }
        [HttpPost]
        public async Task Update(int id)
        {
            var data= await _context.Orders.FindAsync(id);
            data.IsViewed=true;
           await _context.SaveChangesAsync();         
        }
        public async Task<bool> Add(OrderDto data)
        {
            try
            {
                await _context.AddAsync(new Order(data));
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
           }
        }
    }

