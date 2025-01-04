using Business.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace Api.Controllers.Base
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {   
        protected ActionResult RetornoPadrao<T>(ResultadoOperacao<T> data, HttpStatusCode statusCode = HttpStatusCode.OK) where T : class 
        {
            if (data.OperacaoValida)
            {
                return new ObjectResult(data.Data)
                {
                    StatusCode = (int)statusCode,
                };
            }
            return BadRequest(new
            {
                erros = data.Erros,
            });
        }

        protected ActionResult RetornoPadrao(ResultadoOperacao data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if (data.OperacaoValida)
            {
                return new ObjectResult(data.Mensagem ?? null)
                {
                    StatusCode = (int)statusCode,
                };
            }
            return BadRequest(new
            {
                erros = data.Erros,
            });
        }

        protected ActionResult RetornoPadrao(ModelStateDictionary modelstate)
        {   
            var erros = modelstate.Values
                        .SelectMany(e => e.Errors)
                        .Select(e => e.Exception?.Message ?? e.ErrorMessage);

            return RetornoPadrao(ResultadoOperacao.Falha(erros));
        }

        protected ActionResult RetornoPadrao(IdentityResult identityResult)
        {
            var erros = identityResult.Errors.Select(e => e.Description);

            return RetornoPadrao(ResultadoOperacao.Falha(erros));
        }
    }
}
