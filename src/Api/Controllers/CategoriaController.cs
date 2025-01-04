using Api.Controllers.Base;
using Api.Dtos;
using AutoMapper;
using Business.Entities;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/categorias")]
    public class CategoriaController(ICategoriaService categoriaService, IMapper mapper) : MainController
    {   
        private readonly ICategoriaService _categoriaService = categoriaService;

        [HttpGet]
        public async Task<ActionResult> ObterTodos()
        {
            var categorias = await _categoriaService.ObterTodos();
            return RetornoPadrao(ResultadoOperacao<IEnumerable<CategoriaDto>>.Sucesso(mapper.Map<IEnumerable<CategoriaDto>>(categorias)));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            var resultadoPadrao = await _categoriaService.ObterPorId(id);
            return RetornoPadrao(ResultadoOperacao<CategoriaDto>.Sucesso(mapper.Map<CategoriaDto>(resultadoPadrao.Data)));
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(CategoriaDto categoriaDto)
        {
            if (!ModelState.IsValid)
                return RetornoPadrao(ModelState);

            var resultadoPadrao = await _categoriaService.Adicionar(mapper.Map<Categoria>(categoriaDto));
            return RetornoPadrao(resultadoPadrao, HttpStatusCode.Created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, CategoriaDto categoriaDto)
        {
            if (id != categoriaDto.Id) 
                return RetornoPadrao(ResultadoOperacao.Falha("Os ids fornecidos não são iguais."));

            var resultadoPadrao = await _categoriaService.Atualizar(mapper.Map<Categoria>(categoriaDto));
            return RetornoPadrao(resultadoPadrao, HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var resultadoPadrao = await _categoriaService.Exluir(id);
            return RetornoPadrao(resultadoPadrao, HttpStatusCode.NoContent);
        }
    }
}
