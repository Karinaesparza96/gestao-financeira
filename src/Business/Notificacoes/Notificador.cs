using Business.Interfaces;

namespace Business.Notificacoes
{
    public class Notificador : INotificador
    {
        private List<Notificacao> _notificacoes { get; } = [];
        public bool TemNotificacao()
        {
            return _notificacoes.Count > 0;
        }

        public List<Notificacao> ObterTodos()
        {
            return _notificacoes;
        }

        public void Adicionar(Notificacao notificacao)
        {
            _notificacoes.Add(notificacao);
        }
    }
}
