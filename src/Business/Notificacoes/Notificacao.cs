namespace Business.Notificacoes
{
    public class Notificacao(string mensagem, TipoNotificacao? tipo = TipoNotificacao.Erro)
    {
        public string? Mensagem { get; } = mensagem;
        public TipoNotificacao? TipoNotificacao { get; } = tipo;
    }
}
