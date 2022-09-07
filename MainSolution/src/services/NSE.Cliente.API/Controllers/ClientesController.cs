using Microsoft.AspNetCore.Mvc;
using NSE.WebApi.Core.Controllers;

namespace NSE.Clientes.API.Controllers
{
    public class ClientesController : MainController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
