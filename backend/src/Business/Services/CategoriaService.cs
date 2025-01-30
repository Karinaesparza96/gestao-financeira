using Business.Entities;
using Business.Entities.Validations;
using Business.Interfaces;
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
                Notificar("Registro não encontrado");
                return null;
            }

            if (!categoria.Default && !AcessoAutorizado(categoria.UsuarioId))
            {
                Notificar("Não é possivel acessar uma categoria de outro usuário.");
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
               Notificar("Registro não encontrado.");
               return;
            }

            if (categoriaBanco.Default)
            {
                Notificar("Não é possível atualizar uma categoria default.");
                return;
            }

            if (!AcessoAutorizado(categoriaBanco.UsuarioId))
            {
                Notificar("Não é possível atualizar uma categoria de outro usuário.");
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
                Notificar("Registro não encontrado");
                return;
            }

            if (categoria.Default)
            {   
                Notificar("Não é possível excluir uma categoria default.");
                return;
            }

            if (!AcessoAutorizado(categoria.UsuarioId))
            {
                Notificar("Não é possível excluir uma categoria de outro usuário.");
                return;
            }

            if (categoria.Transacoes?.Count() > 0)
            {
                Notificar("Não é possivel excluir uma categoria que possui transações lançadas.");
                return;
            }

            await categoriaRepository.Excluir(categoria!);
        }

    }
}
