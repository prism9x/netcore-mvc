using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prism.Application.Abstracts;
using Prism.Application.DTOs;
using Prism.Application.Services;

namespace Prism.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AccountController(IAppUserService appUserService, IRoleService roleService): Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAllAccountPagination(RequestDatatable requestDatatable)
        {
            var data = await appUserService.GetUserByPaginationAsync(requestDatatable);
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> SaveData(string? id)
        {
            AccountDTO accountDto = !string.IsNullOrEmpty(id) ? await appUserService.GetUserByIdAsync(id) : new ();
            ViewBag.Roles = await roleService.GetRoleForDropdownlistAsync();
            return View(accountDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveData(AccountDTO accountDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = await roleService.GetRoleForDropdownlistAsync();
                ModelState.AddModelError("errorsModel", "Invalid model");

                return View(accountDTO);
            }

            var result = await appUserService.SaveAsync(accountDTO);

            if (result.Status)
            {
                return RedirectToAction("", "Account");
            }

            ViewBag.Roles = await roleService.GetRoleForDropdownlistAsync();
            ModelState.AddModelError("errorsModel", result.Message);
            

            return View(accountDTO);

            //return RedirectToAction("Index"); // Redirect to desired action after saving data
        }
    }
}
