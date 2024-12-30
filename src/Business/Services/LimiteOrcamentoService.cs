using Business.Entities;
using Business.Interfaces;
using Business.Services.Base;

namespace Business.Services
{
    public class LimiteOrcamentoService(IAppIdentityUser appIdentityUser, 
                                        INotificador notificador, 
                                        IUsuarioService usuarioService) : BaseService(appIdentityUser, notificador, usuarioService), ILimiteOrcamentoService
    {
        public Task<LimiteOrcamento?> ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Task Adicionar(LimiteOrcamento entity)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(LimiteOrcamento entity)
        {
            throw new NotImplementedException();
        }

        public Task Exluir(int id)
        {
            throw new NotImplementedException();
        }
    }
}
