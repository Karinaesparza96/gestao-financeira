using Api.Controllers.Base;
using Api.Dtos;
using AutoMapper;
using Business.Entities;
using Business.FiltrosBusca;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [Route("api/transacoes")]
    [Authorize]
    public class TransacaoController(ITransacaoService transacaoService, IMapper mapper) : MainController
    {
        private readonly ITransacaoService _transacaoService = transacaoService;

        [HttpGet]
        public async Task<ActionResult> ObterTodos([FromQuery]FiltroTransacao filtroDto)
        {
            var resultadoOperacao = await _transacaoService.ObterTodos(filtroDto);

            return RetornoPadrao(ResultadoOperacao<IEnumerable<TransacaoDto>>
                        .Sucesso(mapper.Map<IEnumerable<TransacaoDto>>(resultadoOperacao.Data)));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> ObterPorId(int id)
        {   
            var resultadoOperacao = await _transacaoService.ObterPorId(id);

            return RetornoPadrao(ResultadoOperacao<TransacaoDto>
                        .Sucesso(mapper.Map<TransacaoDto>(resultadoOperacao.Data)));
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(TransacaoDto transacaoDto)
        {   
            if (!ModelState.IsValid) return RetornoPadrao(ModelState);

            var retornoOperacao = await _transacaoService.Adicionar(mapper.Map<Transacao>(transacaoDto));

            return RetornoPadrao(retornoOperacao, HttpStatusCode.Created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, TransacaoDto transacaoDto)
        {
            if (id != transacaoDto.Id)
            {
                return RetornoPadrao(ResultadoOperacao.Falha("Os ids devem ser iguais."));
            }

            if (!ModelState.IsValid) return RetornoPadrao(ModelState);

            var resultadoOperacao = await _transacaoService.Atualizar(mapper.Map<Transacao>(transacaoDto));

            return RetornoPadrao(resultadoOperacao, HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var resultadoOperacao = await _transacaoService.Exluir(id);
            return RetornoPadrao(resultadoOperacao, HttpStatusCode.NoContent);
        }
    }
}
