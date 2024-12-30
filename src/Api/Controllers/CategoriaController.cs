using Api.Controllers.Base;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/categorias")]
    public class CategoriaController(INotificador notificador) : MainController(notificador)
    {
        [HttpGet]
        public async Task<ActionResult> ObterTodos()
        {
            return Ok();
        }
    }
}
