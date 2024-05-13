using Microsoft.AspNetCore.Mvc;
using Prism.Application.Abstracts;

namespace Prism.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController(ICategoryService categoryService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var data = await categoryService.GetAll();
            return View(data);
        }
    }
}
