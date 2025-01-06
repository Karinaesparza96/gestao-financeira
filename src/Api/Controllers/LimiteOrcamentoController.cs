using Api.Controllers.Base;
using Api.Dtos;
using AutoMapper;
using Business.Entities;
using Business.FiltrosBusca;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/limites-orcamentos")]
    public class LimiteOrcamentoController(ILimiteOrcamentoService limiteOrcamentoService, IMapper mapper) : MainController
    {
        [HttpGet]
        public async Task<ActionResult> ObterTodos([FromQuery]FiltroLimiteOrcamento filtroLimiteOrcamento)
        {
            var limiteOrcamentos = await limiteOrcamentoService.ObterTodos(filtroLimiteOrcamento);
            return RetornoPadrao(ResultadoOperacao<IEnumerable<LimiteOrcamentoDto>>
                .Sucesso(mapper.Map<IEnumerable<LimiteOrcamentoDto>>(limiteOrcamentos)));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LimiteOrcamentoDto>> ObterPorId(int id)
        {
            var result = await limiteOrcamentoService.ObterPorId(id);
            return RetornoPadrao(ResultadoOperacao<LimiteOrcamentoDto>
                .Sucesso(mapper.Map<LimiteOrcamentoDto>(result.Data)));
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(LimiteOrcamentoDto limiteOrcamentoDto)
        {
            if (!ModelState.IsValid) return RetornoPadrao(ModelState);
            var limiteOrcamento = await limiteOrcamentoService.Adicionar(mapper.Map<LimiteOrcamento>(limiteOrcamentoDto));
            return RetornoPadrao(limiteOrcamento);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Atualizar(int id, LimiteOrcamentoDto limiteOrcamentoDto)
        {
            if (id != limiteOrcamentoDto.Id) 
                return RetornoPadrao(ResultadoOperacao.Falha("Os ids fornecidos não são iguais."));

            if (!ModelState.IsValid) 
                return RetornoPadrao(ModelState);

            var result = await limiteOrcamentoService.Atualizar(mapper.Map<LimiteOrcamento>(limiteOrcamentoDto));
            return RetornoPadrao(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Excluir(int id)
        {
            var result =  await limiteOrcamentoService.Exluir(id);
            return RetornoPadrao(result);
        }
    }
}
