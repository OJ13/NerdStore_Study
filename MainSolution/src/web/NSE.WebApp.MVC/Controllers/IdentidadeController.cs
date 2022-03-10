using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Controllers
{
    public class IdentidadeController : Controller
    {
        private readonly IAutenticacaoService _autenticacaoService;
        public IdentidadeController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpGet]
        [Route("nova-conta")]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [Route("nova-conta")]
        public async Task<IActionResult> Registro(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid)
                return View(usuarioRegistro);

            var resposta = _autenticacaoService.Registro(usuarioRegistro);

            if (false) return View(usuarioRegistro);

            return RedirectToAction(actionName: "Index", controllerName: "Home");
                        
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid)
                return View(usuarioLogin);

            var resposta = _autenticacaoService.Login(usuarioLogin);

            if (false) return View(usuarioLogin);

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        [HttpGet]
        [Route("sair")]
        public async Task<IActionResult> Logout(UsuarioLogin usuarioLogin)
        {
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
    }
}