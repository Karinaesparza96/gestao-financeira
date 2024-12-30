using Api.Controllers.Base;
using AutoMapper;
using Business.Dtos;
using Business.Entities;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [Route("api/transacoes")]
    [Authorize]
    public class TransacaoController(ITransacaoService transacaoService, INotificador notificador, IMapper mapper) : MainController(notificador)
    {
        private readonly ITransacaoService _transacaoService = transacaoService;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransacaoDto>>> ObterTodos([FromQuery]FiltroTransacaoDto filtroDto)
        {
            var transacoes = await _transacaoService.ObterTodos(filtroDto);
            return RetornoPadrao(HttpStatusCode.OK, _mapper.Map<IEnumerable<TransacaoDto>>(transacoes));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TransacaoDto?>> ObterPorId(int id)
        {   
            var transacao = await _transacaoService.ObterPorId(id);
            return RetornoPadrao(HttpStatusCode.OK, _mapper.Map<TransacaoDto>(transacao));
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(TransacaoDto transacaoDto)
        {   
            if (!ModelState.IsValid) return RetornoPadrao(ModelState);

            var transacao = _mapper.Map<Transacao>(transacaoDto);
            await _transacaoService.Adicionar(transacao);

            return RetornoPadrao(HttpStatusCode.Created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, TransacaoDto transacaoDto)
        {
            if (id != transacaoDto.Id)
            {
                NotificarErros("Os ids devem ser iguais.");
                return RetornoPadrao();
            }

            if (!ModelState.IsValid) return RetornoPadrao(ModelState);

            var transacao = _mapper.Map<Transacao>(transacaoDto);
            await _transacaoService.Atualizar(transacao);

            return RetornoPadrao(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            await _transacaoService.Exluir(id);
            return RetornoPadrao(HttpStatusCode.NoContent);
        }
    }
}
