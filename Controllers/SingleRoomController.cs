using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{

    public class SingleRoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
