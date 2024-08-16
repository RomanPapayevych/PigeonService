using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project.Enums;
using Project.Models;
using Project.ViewModels;

namespace Project.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO, string? ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(loginDTO);
            }
            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email!, loginDTO.Password!, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                ApplicationUser? applicationUser = await _userManager.FindByEmailAsync(loginDTO.Email!);
                if (applicationUser != null)
                {
                    if(await _userManager.IsInRoleAsync(applicationUser, UserTypeOptions.Admin.ToString()))
                    {
                        return RedirectToAction("Index", "Home");   
                    }
                }
                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                {
                    return LocalRedirect(ReturnUrl);
                }
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            ModelState.AddModelError("Login", "Invalid email or password");
            return View(loginDTO);
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        /*
         [HttpPost]
        public async Task<IActionResult> Registration(RegistrationDTO registrationDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);   
                return View(registrationDTO);
            }
            ApplicationUser applicationUser = new ApplicationUser() { Email = registrationDTO.Email, PhoneNumber = registrationDTO.Phone, UserName = registrationDTO.Email, PersonName = registrationDTO.PersonName};
            IdentityResult result = await _userManager.CreateAsync(applicationUser, registrationDTO.Password!);
            if (result.Succeeded)
            {
                if(registrationDTO.UserType == Enums.UserTypeOptions.Admin)
                {
                    if(await _roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole() { Name = UserTypeOptions.Admin.ToString() };
                        await _roleManager.CreateAsync(applicationRole);
                    }
                    await _userManager.AddToRoleAsync(applicationUser, UserTypeOptions.Admin.ToString());
                }
                else
                {
                    if(await _roleManager.FindByNameAsync(UserTypeOptions.User.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole() { Name = UserTypeOptions.User.ToString() };
                        await _roleManager.CreateAsync(applicationRole);
                    }
                    await _userManager.AddToRoleAsync(applicationUser, UserTypeOptions.User.ToString());
                }
                await _signInManager.SignInAsync(applicationUser, isPersistent: false);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("Registration", error.Description);
                }
                return View(registrationDTO);
            }
        }
        */
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationDTO registrationDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);   
                return View(registrationDTO);
            }
            ApplicationUser applicationUser = new ApplicationUser() { Email = registrationDTO.Email, PhoneNumber = registrationDTO.Phone, UserName = registrationDTO.Email, PersonName = registrationDTO.PersonName};
            IdentityResult result = await _userManager.CreateAsync(applicationUser, registrationDTO.Password!);
            

            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(applicationUser);

                if (registrationDTO.Email?.ToLower() == "administratorsite@gmail.com")
                {
                    if (await _roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole() { Name = UserTypeOptions.Admin.ToString() };
                        await _roleManager.CreateAsync(applicationRole);
                    }
                    await _userManager.AddToRoleAsync(applicationUser, UserTypeOptions.Admin.ToString());
                }
                else
                {
                    if (await _roleManager.FindByNameAsync(UserTypeOptions.User.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole() { Name = UserTypeOptions.User.ToString() };
                        await _roleManager.CreateAsync(applicationRole);
                    }
                    await _userManager.AddToRoleAsync(applicationUser, UserTypeOptions.User.ToString());
                }
                await _signInManager.SignInAsync(applicationUser, isPersistent: false);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("Registration", error.Description);
                }
                return View(registrationDTO);
            }
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            ApplicationUser? applicationUser = await _userManager.FindByEmailAsync(email);  
            if(applicationUser == null)
            {
                return Json(true); //valid
            }
            else
            {
                return Json(false); //invalid
            }
        }
        public IActionResult Admin()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
        
        public async Task<IActionResult> Delete(string id)
        {
            var users = await _userManager.FindByIdAsync(id);
            if(users == null)
            {
                return NotFound();
            }
            await _userManager.DeleteAsync(users!);
            return View(users);
            
        }
    }
}
