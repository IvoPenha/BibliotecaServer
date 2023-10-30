namespace BibliotecaServer.Core.Validations.Message
{
    public static class ErrorMessages
    {
        public const string UsuarioNotFound
            = "Não existe nenhum usuário com o id informado.";

        public static string UsuarioInvalid(string errors)
            => "Os campos informados para o usuário estão inválidos" + errors;

        public const string LivroNotFound
            = "Não existe nenhum livro com o id informado.";

        public static string LivroInvalid(string errors)
            => "Os campos informados para o livro estão inválidos" + errors;

        public const string EmprestimoNotFound
            = "Não existe nenhum emprestimo com o id informado.";

        public static string EmprestimoInvalid(string errors)
            => "Os campos informados para o emprestimo estão inválidos" + errors;

        public const string EmprestimoLate
            = "O emprestimo está atrasado. Fale com o funcionário";

        public const string LivroNotAvailable
            = "O livro não está disponível para emprestimo";

        public const string LivroAlreadyAvailable
            = "O livro já está disponivel";

        public const string IdMismatch
            = "O id informado não é o mesmo do objeto";

    }
}
