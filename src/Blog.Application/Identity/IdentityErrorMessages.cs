namespace Blog.Application.Identity
{
    internal class IdentityErrorMessages
    {
        public const string IdentityUserAlreadyExists = "Já existe um usuário cadastrado com este e-mail";
        public const string IdentityUserNotFound = "Nenhum usuário com esta identidade foi encontrado";
        public const string IncorrectUserName = "Nome de usuário ou senha incorretos. Tente novamente";
        public const string IncorrectPassword = "Nome de usuário ou senha incorretos. Tente novamente";
        public const string LockoutOnFailure = "Usuário temporariamente bloqueado por tentativas inválidas";
        public const string UserProfileNotFound = "Nenhum perfil de usuário foi encontrado";
        public const string UnathorizedAccountRemoval = "Não tem permissão para remover essa conta";
    }
}
