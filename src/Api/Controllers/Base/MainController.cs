using Business.Interfaces;
using Business.Notificacoes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using Api.Dtos;

namespace Api.Controllers.Base
{
    [ApiController]
    public abstract class MainController(INotificador notificador) : ControllerBase
    {   
        protected ActionResult RetornoPadrao(HttpStatusCode statusCode = HttpStatusCode.OK, object? data = null)
        {
            var response = new ApiResponse()
            {
                Data = data,
                Avisos = notificador.ObterPorTipo(TipoNotificacao.Aviso).Select(x => x.Mensagem)!,
                Erros = notificador.ObterPorTipo(TipoNotificacao.Erro).Select(x => x.Mensagem)!
            };

            if (response.Erros?.Count() > 0)
            {
                return BadRequest(response);
            }

            return new ObjectResult(response)
            {
                StatusCode = (int)statusCode
            };
        }

        protected void NotificarErro(string erro)
        {
            notificador.Adicionar(new Notificacao(erro));
        }

        protected void NotificarErro(ModelStateDictionary modelstate)
        {
            foreach (var msg in modelstate.Values
                         .SelectMany(e => e.Errors)
                         .Select(e => e.Exception?.Message ?? e.ErrorMessage))
            {
                NotificarErro(msg);
            }
        }

        protected void NotificarErro(IdentityResult identityResult)
        {
            foreach (var erro in identityResult.Errors.Select(e => e.Description))
            {
                NotificarErro(erro);
            }
        }
    }
}
