using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Services;
using System;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Controllers
{
    public class CatalogoController : MainController
    {
        //private readonly ICatalogoServiceRefit _catalogoService;
        private readonly ICatalogoService _catalogoService;
        //public CatalogoController(ICatalogoServiceRefit catalogoService)
        public CatalogoController(ICatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        [HttpGet]
        [Route("")]
        [Route("vitrine")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var produtos = await _catalogoService.ObterTodos();

                return View(produtos);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("produto-detalhe/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id)
        {
            var produto = await _catalogoService.ObterPorId(id);

            return View(produto);
        }
    }
}
