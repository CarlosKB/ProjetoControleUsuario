using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class Contato : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
