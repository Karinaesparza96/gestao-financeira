using Api.Controllers.Base;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

//[Authorize]
[Route("api/relatorios")]
public class RelatorioController(INotificador notificador) : MainController(notificador)
{
    [HttpGet]
    public async Task<ActionResult> ObterResumoFinanceiro()
    {
        return Ok();
    }
    
}