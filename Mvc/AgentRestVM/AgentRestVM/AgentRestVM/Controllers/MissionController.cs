using AgentRestVM.Service;
using Microsoft.AspNetCore.Mvc;

namespace AgentRestVM.Controllers
{
    public class MissionController(IGetAllMissionService getAllMissionService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {

                return View(await getAllMissionService.GetAllTask());
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                return View(await getAllMissionService.DetailsMissoin(id));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }

        }

        public async Task<IActionResult> DetailsDashboard()
        {
            try
            {
                return View(await getAllMissionService.GetDashboard());
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
    }
}
