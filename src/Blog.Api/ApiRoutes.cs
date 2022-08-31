namespace Blog.Api
{
    public class ApiRoutes
    {
        public const string BaseRoute = "api/v{version:ApiVersion}/[controller]";


        public static class Identity
        {
            public const string Login = "login";
            public const string Register = "registar-se";
            public const string RegisterUserAdmin = "novo-usuario";
            public const string DeleteAccount = "excluir-minha-conta/{identityUserId}";
            public const string RemoveUserAccount = "remover-usuario/{identityUserId}";
        }

        public static class UserProfile
        {
            public const string GetAllRegisteredUserProfiles = "obter-usuarios-registados";
            public const string GetAllAdminUserProfiles = "obter-usuarios";
            public const string GetUserProfileById = "obter-usuario-por-id/{userProfileId}";
            public const string UpdateUserProfile = "actualizar-perfil-de-usuario-por-id/{userProfileId}";
        }

        public static class Category
        {
            public const string GetCategoryById = "obter-categoria-por-id/{categoryId}";
            public const string AddCategory = "nova-categoria";
            public const string UpdateCategory = "actualizar-categoria/{categoryId}";
            public const string RemoveCategory = "remover-categoria/{categoryId}";
        }

        public static class Post
        {
            public const string GetPostById = "obter-noticia-por-id/{postId}";
            public const string CreatePost = "nova-noticia";
            public const string RemovePost = "remover-noticia/{postId}";
            public const string UpdatePost = "actualizar-noticia/{postId}";
        }
    }
}
