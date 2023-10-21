using Booking.Appliaction.Service.Interface;
using Booking.Application.Service.Interface;
using Booking.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.web.Controllers.Area.Admin
{
    public class AmenityController : Controller
    {
        private readonly IAmenitySerivce amenitySerivce;
        private readonly IVillaService villaService;

        public AmenityController(IAmenitySerivce amenitySerivce ,IVillaService villaService)
        {
            this.amenitySerivce = amenitySerivce;
            this.villaService = villaService;
        }
        public IActionResult Index()
        {
            var amenity= amenitySerivce.GetAllSerivce();
            return View(amenity);
        }
        [HttpGet]
        public IActionResult Create() 
        {

            ViewBag.Villa = new SelectList(villaService.GetAllSerivce(), "ID", "VillaName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Amenity amenity) 
        {
            if (ModelState.IsValid)
            {
                amenitySerivce.AddAmenitySerivce(amenity);

                return RedirectToAction("Index");
            }
            return View(amenity);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var amenity = amenitySerivce.GetByIdSerivce(id);
            ViewBag.Villa = new SelectList(villaService.GetAllSerivce(), "ID", "VillaName");

            return View(amenity);
        }
        [HttpPost]
        public IActionResult Edit(int id,Amenity amenity)
        {
            if (ModelState.IsValid)
            {
                amenitySerivce.UpdateAmenitySerivce(amenity);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Villa = new SelectList(villaService.GetAllSerivce(), "ID", "VillaName");
                return View(amenity);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id) 
        {
            var DeleteAmenity = amenitySerivce.GetByIdSerivce(id);
            ViewBag.Villa = new SelectList(villaService.GetAllSerivce(), "ID", "VillaName");
            return View(DeleteAmenity);
        }
        [HttpPost]
        public IActionResult Delete(Amenity amenity)
        {
            var DeleteAmenity = amenitySerivce.GetByIdSerivce(amenity.Id);
            if (DeleteAmenity != null)
            {
                var objamenity = amenitySerivce.DeleteAmenitySerivce(amenity.Id);
                if (objamenity == true)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.ErrorMessage = "The Amenity was not found for deletion.";
                return View();
            }
            else
            {
                ViewBag.ErrorMessage = "The Amenity was not found for deletion.";

                return View();
            }
        }
    }
}
