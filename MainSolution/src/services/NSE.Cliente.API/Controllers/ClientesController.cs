using Microsoft.AspNetCore.Mvc;
using NSE.Clientes.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.WebApi.Core.Controllers;
using System;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Controllers
{
    public class ClientesController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;
        public ClientesController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("clientes")]
        public async Task<IActionResult> Index()
        {
            var result = await _mediatorHandler.EnviarCommando(
                new RegistrarClienteCommand(
                    id: Guid.NewGuid(), 
                    nome: "Adonis Creed", 
                    email: "adoniscreed@mail.com", 
                    cpf: "45488219021")
                );

            return CustomResponse(result);
        }
    }
}
