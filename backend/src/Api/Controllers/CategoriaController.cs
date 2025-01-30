using Api.Controllers.Base;
using Api.Dtos;
using AutoMapper;
using Business.Entities;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Business.Utils;

namespace Api.Controllers
{
    //[Authorize]
    [Route("api/categorias")]
    public class CategoriaController(ICategoriaService categoriaService, IMapper mapper, INotificador notificador) : MainController(notificador)
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> ObterTodos()
        {
            var categorias = await categoriaService.ObterTodos();

            using (TextWriter tw = new StreamWriter("d:\\output.csv"))
            {
                // podemos retornar um base64 a partir deste método
                string lcCsvString = ExportHelper.getCSV<Categoria>(categorias.ToList());


                // este método é uma variação para gerar o arquivo fisico e validar sua estrutura.
                ExportHelper.CreateCSV<Categoria>(categorias.ToList(), "d:\teste4.csv");
                string content = System.IO.File.ReadAllText(@"d:\teste4.csv");
            }

            return RetornoPadrao(data: mapper.Map<IEnumerable<CategoriaDto>>(categorias));
            //return tiago;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoriaDto>> ObterPorId(int id)
        {
            var categoria = await categoriaService.ObterPorId(id);
            return RetornoPadrao(data: mapper.Map<CategoriaDto>(categoria));
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(CategoriaDto categoriaDto)
        {
            if (!ModelState.IsValid)
            {
                NotificarErro(ModelState);
                return RetornoPadrao();
            }

            await categoriaService.Adicionar(mapper.Map<Categoria>(categoriaDto));
            return RetornoPadrao(HttpStatusCode.Created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, CategoriaDto categoriaDto)
        {
            if (id != categoriaDto.Id)
            {
                NotificarErro("Os ids fornecidos não são iguais.");
                return RetornoPadrao();
            }

            await categoriaService.Atualizar(mapper.Map<Categoria>(categoriaDto));
            return RetornoPadrao(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            await categoriaService.Exluir(id);
            return RetornoPadrao(HttpStatusCode.NoContent);
        }
    }
}
