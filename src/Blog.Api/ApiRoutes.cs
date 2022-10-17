﻿namespace Blog.Api
{
    public class ApiRoutes
    {
        public const string BaseRoute = "api/v{version:ApiVersion}/[controller]";


        public static class Identity
        {
            public const string GetAllRoles = "obter-tipos-de-usuarios";
            public const string GetCurrentUser = "obter-usuario-logado";
            public const string Login = "login";
            public const string Logout = "logout";
            public const string Register = "registar-se";
            public const string CreateUser = "novo-usuario";
            public const string DeleteAccount = "excluir-minha-conta/{identityUserId}";
            public const string RemoveUser = "remover-usuario/{identityUserId}";
        }

        public static class UserProfile
        {
            public const string GetPublicUserProfiles = "obter-usuarios-publicos";
            public const string GetAdminUserProfiles = "obter-usuarios-admins";
            public const string GetUserProfileById = "obter-usuario-por-id/{userProfileId}";
            public const string UpdateUserProfile = "actualizar-usuario/{identityId}";
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

            public const string GetPostsByCategoryId = "obter-noticias-por-categoria/{categoryId}";
            public const string GetPostById = "obter-noticia-por-id/{postId}";
            public const string CreatePost = "nova-noticia";
            public const string RemovePost = "remover-noticia/{postId}";
            public const string UpdatePost = "actualizar-noticia/{postId}";
            public const string UploadImage = "enviar-imagem";

            //Comments
            public const string GetAllPostComments = "{postId}/comentarios";
            public const string GetCommentById = "{postId}/comentario/{commentId}";
            public const string RemovePostComment = "{postId}/remover-comentario/{commentId}";
            public const string UpdateComment = "{postId}/editar-comentario/{commentId}";
            public const string AddPostComment = "{postId}/adicionar-comentario";

            //Reactions
            public const string GetAllPostReactions = "{postId}/reacoes";
            public const string AddPostReaction = "{postId}/adicionar-reacao";
            public const string RemovePostReaction = "{postId}/remover-reacao/{reactionId}";

            //Comment Response
            public const string GetAllCommentAnswers = "{postId}/comentario/{commentId}/respostas";
            public const string GetCommentAnswerById = "{postId}/comentario/{commentId}/resposta/{commentAnswerId}";
            public const string AddCommentAnswer = "{postId}/comentario/{commentId}/adicionar-resposta";
            public const string UpdateCommentAnswer = "{postId}/comentario/{commentId}/atualizar-resposta/{commentAnswerId}";
            public const string RemoveCommentAnswer = "{postId}/comentario/{commentId}/remover-resposta/{commentAnswerId}";

            //Comment Reaction
            public const string GetAllCommentReactions = "{postId}/comentario/{commentId}/reacoes";
            public const string AddCommentReaction = "{postId}/comentario/{commentId}/adicionar-reacao";
            public const string RemoveCommentReaction = "{postId}/comentario/{commentId}/remover-reacao/{reactionId}";
        }
    }
}
