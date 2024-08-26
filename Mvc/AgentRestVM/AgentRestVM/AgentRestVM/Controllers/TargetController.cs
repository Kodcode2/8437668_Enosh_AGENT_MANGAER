using AgentRestVM.Service;
using Microsoft.AspNetCore.Mvc;

namespace AgentRestVM.Controllers
{
    public class TargetController(ITargetService targetService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await targetService.getAllTargetVM());
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
