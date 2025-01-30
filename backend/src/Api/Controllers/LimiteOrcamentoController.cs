using System.ComponentModel;
using System.Net;
using Api.Controllers.Base;
using Api.Dtos;
using AutoMapper;
using Business.Entities;
using Business.FiltrosBusca;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/limites-orcamentos")]
    public class LimiteOrcamentoController(ILimiteOrcamentoService limiteOrcamentoService, IMapper mapper, INotificador notificador) : MainController(notificador)
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LimiteOrcamentoDto>>> ObterTodos([FromQuery]FiltroLimiteOrcamento filtroLimiteOrcamento)
        {
            var limiteOrcamentos = await limiteOrcamentoService.ObterTodos(filtroLimiteOrcamento);
            return RetornoPadrao(data: mapper.Map<IEnumerable<LimiteOrcamentoDto>>(limiteOrcamentos));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LimiteOrcamentoDto>> ObterPorId(int id)
        {
            var limiteOrcamento = await limiteOrcamentoService.ObterPorId(id);
            return RetornoPadrao(data: mapper.Map<LimiteOrcamentoDto>(limiteOrcamento));
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(LimiteOrcamentoDto limiteOrcamentoDto)
        {
            if (!ModelState.IsValid)
            {
                NotificarErro(ModelState);
                return RetornoPadrao();
            }

            await limiteOrcamentoService.Adicionar(mapper.Map<LimiteOrcamento>(limiteOrcamentoDto));
            return RetornoPadrao(HttpStatusCode.Created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Atualizar(int id, LimiteOrcamentoDto limiteOrcamentoDto)
        {
            if (id != limiteOrcamentoDto.Id)
            {
                NotificarErro("Os ids fornecidos não são iguais.");
                return RetornoPadrao();
            }

            if (!ModelState.IsValid)
            {
                NotificarErro(ModelState);
                return RetornoPadrao();
            }

            await limiteOrcamentoService.Atualizar(mapper.Map<LimiteOrcamento>(limiteOrcamentoDto));
            return RetornoPadrao(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Excluir(int id)
        {
            await limiteOrcamentoService.Exluir(id);
            return RetornoPadrao(HttpStatusCode.NoContent);
        }
    }
}
