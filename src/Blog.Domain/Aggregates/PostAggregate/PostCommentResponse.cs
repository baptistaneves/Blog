﻿using Blog.Domain.Aggregates.UserProfileAggregate;
using Blog.Domain.Exceptions.Posts;
using Blog.Domain.Validatores.Posts;

namespace Blog.Domain.Aggregates.PostAggregate
{
    public class PostCommentResponse
    {
        private PostCommentResponse() { }

        public Guid PostCommentResponseId { get; private set; }
        public Guid PostCommentId { get; private set; }
        public Guid UserProfileId { get; private set; }
        public string Text { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastModified { get; private set; }

        //EF - Relation
        public UserProfile UserProfile { get; private set; }
        public PostComment PostComment { get; private set; }

        /// <summary>
        /// Create post comment response
        /// </summary>
        /// <param name="userProfileId">User profile ID</param>
        /// <param name="commentId">Comment ID</param>
        /// <param name="text">Text post comment response</param>
        /// <returns see cref="PostCommentResponse"></returns>
        public static PostCommentResponse CreateCommentResponse(Guid userProfileId, Guid commentId, string text)
        {
            var validator = new PostCommentResponseValidator();

            var objToValidate = new PostCommentResponse
            {
                UserProfileId = userProfileId,
                PostCommentId = commentId,
                Text = text,
                CreatedAt = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };

            var validationResult = validator.Validate(objToValidate);
            if (validationResult.IsValid) return objToValidate;

            var exception = new PostCommentResponseNotValidException("");

            validationResult.Errors.ForEach(error => exception.ValidationErrors.Add(error.ErrorMessage));

            throw exception;
        }

        /// <summary>
        /// Update comment response text
        /// </summary>
        /// <param name="newText">Text comment</param>
        public void UpdateCommentResponseText(string newText)
        {
            if(!string.IsNullOrEmpty(newText))
            {
                var exception = new PostCommentResponseNotValidException("Resposta não pode ser actualizada. " +
                    "O texto digitado não é válido");

                exception.ValidationErrors.Add("O texto digitado é nulo ou contém apenas espaços vázios");

                throw exception;
            }

            Text = newText;
            LastModified = DateTime.UtcNow;
        }
    }
}
