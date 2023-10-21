using Booking.Appliaction.Service.Interface;
using Booking.Application.Service.Interface;
using Booking.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.web.Controllers.Area.Admin
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService villaNumberService;
        private readonly IVillaService villaService;

        public VillaNumberController(IVillaNumberService villaNumberService ,IVillaService villaService)
        {
            this.villaNumberService = villaNumberService;
            this.villaService = villaService;
        }

        public IActionResult Index()
        {
             var  villaNumber= villaNumberService.GetAllVillaNumbersSerivce();
            return View(villaNumber);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Villa = new SelectList(villaService.GetAllSerivce(), "ID", "VillaName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VillaNumber villaNumber)
        {
            if(ModelState.IsValid) 
            {
                var ExitedObj = villaNumberService.CheckVillaNumberSerivce(villaNumber.RoomNumber);

                if (ExitedObj)
                {
                    ViewBag.ErrorMessage = "The Room Number is Exited";
                    ViewBag.Villa = new SelectList(villaService.GetAllSerivce(), "ID", "VillaName");
                    return View(villaNumber);
                }
                else
                {
                    villaNumberService.AddVillaNumberSerivce(villaNumber);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.Villa = new SelectList(villaService.GetAllSerivce(), "ID", "VillaName");
                return View(villaNumber);

            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var EditVillaNumber= villaNumberService.GetVillaNumberByIdSerivce(id);
            ViewBag.Villa = new SelectList(villaService.GetAllSerivce(), "ID", "VillaName");
            return View(EditVillaNumber);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id ,VillaNumber villaNumber)
        {
            if (ModelState.IsValid)
            {
                var EditVillaNumber = villaNumberService.GetVillaNumberByIdSerivce(id);
                
                if (EditVillaNumber != null)
                {
                    villaNumberService.UpdateVillaNumberSerivce(villaNumber);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Fill right Data";
                    return View(villaNumber);
                }
            }
            else
            {
                return View(villaNumber);

            }
        }



        [HttpGet]
        public IActionResult Delete(int id) 
        {
           var objdelete=  villaNumberService.GetVillaNumberByIdSerivce(id);
            if(objdelete == null)
            {
                ViewBag.ErrorMessage = "ID dosnot founded";
            }
            return View(objdelete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(VillaNumber villaNumber) 
        {
            var objdelete = villaNumberService.GetVillaNumberByIdSerivce(villaNumber.ID);
            if(objdelete != null) 
            {
                villaNumberService.DeleteVillaNumberSerivce(villaNumber.ID);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "ID dosnot founded";
                return View(villaNumber);
            }
        }

    }
}
