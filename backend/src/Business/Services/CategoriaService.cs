using Business.Entities;
using Business.Entities.Validations;
using Business.Interfaces;
using Business.Messages;
using Business.Services.Base;

namespace Business.Services
{
    public class CategoriaService(IAppIdentityUser appIdentityUser,
                                ICategoriaRepository categoriaRepository,
                                INotificador notificador) : BaseService(appIdentityUser, notificador), ICategoriaService
    {
        public async Task<IEnumerable<Categoria>> ObterTodos()
        {
            var categorias = await categoriaRepository.Buscar(predicate: x => x.Default || x.UsuarioId == UsuarioId, 
                                                                orderBy: x => x.Default);
            return categorias;
        }
        public async Task<Categoria?> ObterPorId(Guid id)
        {   
            var categoria = await categoriaRepository.ObterPorId(id);

            if (categoria == null)
            {   
                Notificar(Mensagens.RegistroNaoEncontrado);
                return null;
            }

            if (!categoria.Default && !AcessoAutorizado(categoria.UsuarioId))
            {
                Notificar(Mensagens.AcaoNaoAutorizada);
                return null;
            }

            return categoria;
        }

        public async Task Adicionar(Categoria categoria)
        {
           if(!ExecutarValidacao(new CategoriaValidation(), categoria)) return;

            categoria.UsuarioId = UsuarioId;
            categoria.Default = false;

            await categoriaRepository.Adicionar(categoria);
        }

        public async Task Atualizar(Categoria categoria)
        {
            if (!ExecutarValidacao(new CategoriaValidation(), categoria)) return;

            var categoriaBanco = await categoriaRepository.ObterPorId(categoria.Id);

            if (categoriaBanco == null)
            {
                Notificar(Mensagens.RegistroNaoEncontrado);
                return;
            }

            if (categoriaBanco.Default)
            {
                Notificar(Mensagens.AcaoNaoAutorizadaCategoriaDefault);
                return;
            }

            if (!AcessoAutorizado(categoriaBanco.UsuarioId))
            {
                Notificar(Mensagens.AcaoNaoAutorizada);
                return;
            }

            categoriaBanco.Nome = categoria.Nome;

            await categoriaRepository.Atualizar(categoriaBanco);
        }

        public async Task Excluir(Guid id)
        {
            var categoria = await categoriaRepository.ObterTransacoes(id);

            if (categoria == null)
            {
                Notificar(Mensagens.RegistroNaoEncontrado);
                return;
            }

            if (categoria.Default)
            {   
                Notificar(Mensagens.AcaoNaoAutorizadaCategoriaDefault);
                return;
            }

            if (!AcessoAutorizado(categoria.UsuarioId))
            {
                Notificar(Mensagens.AcaoNaoAutorizada);
                return;
            }

            if (categoria.Transacoes?.Count() > 0)
            {
                Notificar(Mensagens.AcaoNaoAutorizadaExcluirCategoria);
                return;
            }

            await categoriaRepository.Excluir(categoria!);
        }

    }
}
