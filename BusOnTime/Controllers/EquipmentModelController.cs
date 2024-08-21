using Microsoft.AspNetCore.Mvc;

namespace BusOnTime.Web.Controllers
{
    public class EquipmentModelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
