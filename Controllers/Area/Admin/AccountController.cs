using Booking.Domain.Entity;
using Booking.web.Models;
using Booking.web.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Booking.web.Controllers.Area.Admin
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {

            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            if (!roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
               // roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).Wait();
                roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).Wait();
                roleManager.CreateAsync(new IdentityRole(SD.Role_Owner)).Wait();

            }
            RegisterVM registerVM = new RegisterVM()
            {
                RoleList = roleManager.Roles.Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                }),
            };
            return View(registerVM);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Name = registerVM.Name,
                    Email = registerVM.Email,
                    DateOfBirth = registerVM.DateOfBirth,
                    CreatedAt = DateTime.Now,
                    UserName = registerVM.Email,
                    PhoneNumber = registerVM.PhoneNumber,
                    PhoneNumberConfirmed = true,
                    NormalizedEmail = registerVM.Email.ToUpper(),
                    EmailConfirmed = true,
                    //LockoutEnabled = false,
                    //LockoutEnd = DateTime.UtcNow.AddYears(2)
                };

                var result = await userManager.CreateAsync(user, registerVM.Password);

                if (result.Succeeded)
                {

                    if (!string.IsNullOrEmpty(registerVM.Role))
                    {
                        await userManager.AddToRoleAsync(user, registerVM.Role);
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(user, SD.Role_Customer);

                    }
                    await signInManager.SignInAsync(user, isPersistent: true);
                    return RedirectToAction("Index");
                }
            }
            registerVM.RoleList = roleManager.Roles.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = x.Name,
                Value = x.Name
            });
            return View(registerVM);
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginVm loginVm = new LoginVm();
            return View(loginVm) ;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            if (ModelState.IsValid)
            {
                var result =  await signInManager.PasswordSignInAsync(loginVm.Email, loginVm.Password, loginVm.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {

                    var user = await userManager.FindByEmailAsync(loginVm.Email);
                    if (await userManager.IsInRoleAsync(user, SD.Role_Admin))
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else if( await userManager.IsInRoleAsync(user,SD.Role_Owner))
                    {
                        return RedirectToAction("Index","Property Owner");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ViewBag.error = "Email or password is in vaild";
                    return View(loginVm);
                }
            }
            return View(loginVm);
        }
    }
}
