using Microsoft.AspNetCore.Mvc;

namespace Inventario.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
