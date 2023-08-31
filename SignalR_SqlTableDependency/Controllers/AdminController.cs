using Microsoft.AspNetCore.Mvc;
using SignalR_SqlTableDependency.BL;

namespace SignalR_SqlTableDependency.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminJobs _adminJobs;

        public AdminController(AdminJobs adminJobs)
        {
            _adminJobs = adminJobs;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessLoans()
        {
            try
            {
                await _adminJobs.ProcessLoans();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
