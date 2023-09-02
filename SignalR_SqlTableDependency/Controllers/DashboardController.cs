using Microsoft.AspNetCore.Mvc;

namespace SignalR_SqlTableDependency.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Customer()
        {
            return View();
        }
        public IActionResult Sale()
        {
            return View();
        }
    }
}
