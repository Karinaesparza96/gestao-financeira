namespace Business.Messages
{
    public static class Mensagens
    {
        public static string RegistroNaoEncontrado = "Registro não encontrado.";

        public static string AcaoNaoAutorizada = "Não é possível realizar operações em um registro de outro usuário.";

        public static string AcaoNaoAutorizadaCategoriaDefault = "Não é possível realizar operações que modifiquem uma categoria default.";

        public static string AcaoNaoAutorizadaExcluirCategoria = "Não é possivel excluir uma categoria que possui transações lançadas.";

        public static string SemRecursoDisponivel = "Não é possivel definir um limite que excede os recursos disponíveis.";

        public static string ExisteLimiteGeral = "Já existe um limite geral definido para este período.";

        public static string CategoriaNaoCadastrada = "Categoria precisa ser cadastrada antes de associar a uma transação.";

        public static string UsuarioEouSenhaIncorretos = "Usuário e ou senha incorretos.";

        public static string IdsDiferentes = "Os ids fornecidos não são iguais.";

        public static string CategoriaJaCadastrada = "Categoria já está cadastrada.";

        public static string JaExisteCategoriaComEsseNome = "Já existe outra categoria com esse nome.";
    }
}
