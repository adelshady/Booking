using Booking.Application.Service.Implemntation;
using Booking.Application.Service.Interface;
using Booking.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Booking.web.Controllers.Area.Admin
{
    public class VillaController : Controller
    {
        IVillaService villaService;
        public VillaController(IVillaService villaService)
        {
            this.villaService = villaService;
        }
        public IActionResult Index()
        {
            var villa = villaService.GetAllSerivce();
            return View(villa);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Villa ObjVilla)
        {
            if (ModelState.IsValid)
            {
                villaService.AddVillaSerivce(ObjVilla);
                return RedirectToAction("Index");
            }
            return View(ObjVilla);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var villa = villaService.GetByIdSerivce(id);
            return View(villa);
        }
        [HttpPost]
        public IActionResult Edit(Villa villa)
        {
            if (ModelState.IsValid)
            {
                villaService.UpdateVillaSerivce(villa);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Villa? obj = villaService.GetByIdSerivce(id);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(Villa villa)
        {
            bool delete = villaService.DeleteVillaSerivce(villa.ID);
            if (delete)
            {
                return RedirectToAction("Index");
            }

            return View(villa);
        }




    }
}
