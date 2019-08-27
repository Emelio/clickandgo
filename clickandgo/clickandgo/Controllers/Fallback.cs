
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace apGlobal.api.Controllers
{
    public class Fallback : ControllerBase
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "index.html"), "text/html");
        }
    }
}