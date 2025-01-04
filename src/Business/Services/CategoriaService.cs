using AutoMapper;
using Business.Entities;
using Business.Entities.Validations;
using Business.Interfaces;
using Business.Services.Base;

namespace Business.Services
{
    public class CategoriaService(IAppIdentityUser appIdentityUser,
                                ICategoriaRepository categoriaRepository,
                                IUsuarioService usuarioService) : BaseService(appIdentityUser, usuarioService), ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;
        public async Task<IEnumerable<Categoria>> ObterTodos()
        {
            var categorias = await _categoriaRepository.Buscar(predicate: x => x.Default || x.UsuarioId == UsuarioId, 
                                                                orderBy: x => !x.Default);
            return categorias;
        }
        public async Task<ResultadoOperacao<Categoria>> ObterPorId(int id)
        {   
            var categoria = await _categoriaRepository.ObterPorId(id);

            if (categoria == null)
            {
                return ResultadoOperacao<Categoria>.Falha("Registro não encontrado");
            }

            if (!categoria.Default && !AcessoAutorizado(categoria.UsuarioId))
            {
                return ResultadoOperacao<Categoria>.Falha("Não é possivel acessar uma categoria de outro usuário.");
            }

            return ResultadoOperacao<Categoria>.Sucesso(categoria);
        }

        public async Task<ResultadoOperacao> Adicionar(Categoria categoria)
        {
            var validacao = ExecutarValidacao(new CategoriaValidation(), categoria);

            if (!validacao.OperacaoValida) return ResultadoOperacao.Falha(validacao.Erros);

            var usuario = await ObterUsuarioLogado();

            categoria.Usuario = usuario;
            categoria.Default = false;

            await _categoriaRepository.Adicionar(categoria);

            return ResultadoOperacao.Sucesso();
        }

        public async Task<ResultadoOperacao> Atualizar(Categoria categoria)
        {

            var validacao = ExecutarValidacao(new CategoriaValidation(), categoria);

            if (!validacao.OperacaoValida) return ResultadoOperacao.Falha(validacao.Erros);

            var categoriaBanco = await _categoriaRepository.ObterPorId(categoria.Id);

            if (categoriaBanco == null) return ResultadoOperacao.Falha("Registro não encontrado.");

            if (categoriaBanco.Default)
            {
                return ResultadoOperacao.Falha("Não é possivél atualizar uma categoria default.");
            }

            if (!AcessoAutorizado(categoriaBanco.UsuarioId))
            {
                return ResultadoOperacao.Falha("Não é possivel atualizar uma categoria de outro usuário.");
            }

            categoriaBanco.Nome = categoria.Nome;

            await _categoriaRepository.Atualizar(categoriaBanco);

            return ResultadoOperacao.Sucesso();
        }

        public async Task<ResultadoOperacao> Exluir(int id)
        {
            var categoria = await _categoriaRepository.ObterTransacoes(id);

            if (categoria == null)
            {
                return ResultadoOperacao.Falha("Registro não encontrado."); 
            }

            if (categoria.Default)
            {
                return ResultadoOperacao.Falha("Não é possivél excluir uma categoria default.");
            }

            if (!AcessoAutorizado(categoria.UsuarioId))
            {
                return ResultadoOperacao.Falha("Não é possivel exluir uma categoria de outro usuário.");
            }

            if (categoria?.Transacoes?.Count() > 0)
            {
               return ResultadoOperacao.Falha("Não é possivel excluir uma categoria que possui transações lançadas.");
            }

            await _categoriaRepository.Excluir(categoria);

            return ResultadoOperacao.Sucesso();
        }

    }
}
