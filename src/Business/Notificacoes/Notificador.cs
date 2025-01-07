using Business.Interfaces;

namespace Business.Notificacoes
{
    public class Notificador : INotificador
    {
        private List<Notificacao> _notificacoes { get; } = [];

        public bool TemNotificacao(TipoNotificacao? tipo = null)
        {
            return tipo == null ? _notificacoes.Any() : _notificacoes.Any(n => n.TipoNotificacao == tipo);
        }

        public List<Notificacao> ObterTodos()
        {
            return _notificacoes;
        }

        public List<Notificacao> ObterPorTipo(TipoNotificacao tipo)
        {
            return _notificacoes.Where(n => n.TipoNotificacao == tipo).ToList();
        }

        public void Adicionar(Notificacao notificacao)
        {
            _notificacoes.Add(notificacao);
        }
    }

    public enum TipoNotificacao
    {
        Erro = 1,
        Aviso
    }
}
