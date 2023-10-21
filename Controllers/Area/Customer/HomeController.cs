using Booking.Application.Service.Implemntation;
using Booking.Application.Service.Interface;
using Booking.web.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Booking.web.Controllers.Area.Customer
{
    public class HomeController : Controller
    {
        private readonly IVillaService villaService;

        public HomeController(IVillaService villaService)
        {
            this.villaService = villaService;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Nights = 1,
                VillaList = villaService.GetAllSerivce(),
                CheckInDate= DateOnly.FromDateTime(DateTime.Now),
            };
            return View(homeVM);
        }

        [HttpPost]
        public IActionResult GetVillasByDate(int nights, DateOnly checkInDate)
        {

            HomeVM homeVM = new()
            {
                CheckInDate = checkInDate,
                VillaList = villaService.GetVillasAvailabilityByDate(nights, checkInDate),
                Nights = nights
            };

            return PartialView("_VillaList", homeVM);
        }

    }
}
