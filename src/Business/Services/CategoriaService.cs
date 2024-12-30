using Business.Entities;
using Business.Interfaces;
using Business.Services.Base;

namespace Business.Services
{
    public class CategoriaService(IAppIdentityUser appIdentityUser,
                                INotificador notificador,
                                ICategoriaRepository categoriaRepository,
                                IUsuarioService usuarioService) : BaseService(appIdentityUser, notificador, usuarioService), ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;
        public async Task<Categoria?> ObterPorId(int id)
        {
            var categoria = await _categoriaRepository.ObterPorId(id);

            if (categoria == null)
            {
                NotificarErro("Registro nao encontrado");
                return null;
            }
            if (categoria.Default)
            {
                return categoria;
            }
            var usuarioAutorizado = _appIdentityUser.IsOwner(categoria.UsuarioId);

            if (!usuarioAutorizado)
            {
                NotificarErro("Registro nao encontrado");
                return null;
            }
            return categoria;
        }

        public Task Adicionar(Categoria entity)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(Categoria entity)
        {
            throw new NotImplementedException();
        }

        public Task Exluir(int id)
        {
            throw new NotImplementedException();
        }
    }
}
