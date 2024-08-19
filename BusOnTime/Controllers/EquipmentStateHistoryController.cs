using Microsoft.AspNetCore.Mvc;

namespace BusOnTime.Web.Controllers
{
    public class EquipmentStateHistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
