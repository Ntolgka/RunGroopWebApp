using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly SignInManager<AppUserModel> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<AppUserModel> userManager, SignInManager<AppUserModel> signInManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if(!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

            if(user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);

                if(passwordCheck)
                {
                    //if password is correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Race");
                    }
                }
                //if password is wrong
                TempData["Error"] = "Invalid login attempt.";
            }
            //if user is not found
            TempData["Error"] = "Wrong credentials.";
            return View(loginVM);
        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "Email already exists.";
                return View(registerVM);
            }

            var newUser = new AppUserModel() 
            { 
                UserName = registerVM.EmailAddress, 
                Email = registerVM.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if(newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, "User");

            return RedirectToAction("Login", "Account");
            
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
    
}
