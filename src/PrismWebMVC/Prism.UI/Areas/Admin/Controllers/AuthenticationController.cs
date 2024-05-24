using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Prism.Application.Abstracts;
using Prism.Domain.Entities;
using Prism.UI.Areas.Admin.Models;

namespace Prism.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthenticationController(IAuthenticationService authenticationService, SignInManager<AppUser> signInManager) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            var mdLogin = new LoginModel();
            return View(mdLogin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel mdLogin)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors)
                                                .Select(x => x.ErrorMessage).ToList();

                ViewBag.Errors = string.Join("</br>", errors);
                return View();
            }

            var result = await authenticationService.CheckLogin(mdLogin.Username, mdLogin.Password, mdLogin.HasRemember);

            if (result.Status)
                return RedirectToAction("Index", "Home");

            ViewBag.Error = result.Message;

            return View(mdLogin);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return View("Login");
        }
    }
}