using Microsoft.AspNetCore.Mvc;

namespace Booking.web.Controllers.Area.Admin
{
    public class bookingController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
