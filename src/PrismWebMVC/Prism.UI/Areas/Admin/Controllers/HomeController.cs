using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prism.Application.Abstracts;

namespace Prism.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController() : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
