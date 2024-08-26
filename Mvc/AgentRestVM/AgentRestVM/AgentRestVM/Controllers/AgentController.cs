using AgentRestVM.Service;
using Microsoft.AspNetCore.Mvc;

namespace AgentRestVM.Controllers
{
    public class AgentController(IAgentService agentService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                return View( await agentService.GetAllAgentVM());
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
