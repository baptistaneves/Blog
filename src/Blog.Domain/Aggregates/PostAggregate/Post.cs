using Blog.Domain.Aggregates.UserProfileAggregate;
using Blog.Domain.Entities.Categories;
using Blog.Domain.Exceptions.Posts;
using Blog.Domain.Validatores.Posts;

namespace Blog.Domain.Aggregates.PostAggregate
{
    public class Post
    {
        private readonly List<PostReaction> _reactions = new List<PostReaction>();
        private Post() { }

        public Guid PostId { get; private set; }
        public Guid UserProfileId { get; private set; }
        public Guid CategoryId { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public string Image { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastModified { get; private set; }

        //EF - Relation
        public UserProfile UserProfile { get; private set; }
        public Category Category { get; private set; }
        public IEnumerable<PostReaction> Reactions { get { return _reactions; } }

        private readonly List<PostComment> _comments;
        public IReadOnlyCollection<PostComment> Comments  => _comments;


        /// <summary>
        /// Create post
        /// </summary>
        /// <param name="userProfileId">User profile ID</param>
        /// <param name="categoryId">Category ID</param>
        /// <param name="title">Post Title</param>
        /// <param name="content">Post Content</param>
        /// <param name="image">Post image</param>
        /// <returns see cref="Post"></returns>
        /// <exception cref="PostNotValidException"></exception>
        public static Post CreatePost(Guid userProfileId, Guid categoryId, string title, string content,
            string image)
        {
            var validator = new PostCreateValidator();

            var objToValidate = new Post
            {
                UserProfileId = userProfileId,
                CategoryId = categoryId,
                Title = title,
                Content = content,
                Image = image,
                CreatedAt = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };

            var validationResult = validator.Validate(objToValidate);
            if (validationResult.IsValid) return objToValidate;

            var exception = new PostNotValidException("Erro ao criar notícia");

            validationResult.Errors.ForEach(error => exception.ValidationErrors.Add(error.ErrorMessage));

            throw exception;
        }

        /// <summary>
        /// Update post
        /// </summary>
        /// <param name="userProfileId">User profile ID</param>
        /// <param name="categoryId">Category ID</param>
        /// <param name="title">Post Title</param>
        /// <param name="content">Post Content</param>
        /// <param name="image">Post image</param>
        /// <returns see cref="Post"></returns>
        /// <exception cref="PostNotValidException"></exception>
        public void UpdatePost(Guid categoryId, string title, string content, string image)
        {
            var validator = new PostUpdateValidator();

            var objToValidate = new Post { CategoryId = categoryId, Title = title, Content = content };

            var validationResult = validator.Validate(objToValidate);
            if (!validationResult.IsValid)
            {
                var exception = new PostNotValidException("Erro ao actualizar notícia");

                validationResult.Errors.ForEach(error => exception.ValidationErrors.Add(error.ErrorMessage));

                throw exception;
            }

            CategoryId = categoryId;
            Title = title;
            Content = content;
            Image = image;
            LastModified = DateTime.UtcNow;
        }

        public void AddPostReaction(PostReaction newReaction)
        {
            _reactions.Add(newReaction);
        }

        public void RemovePostReaction(PostReaction toRemove)
        {
            _reactions.Remove(toRemove);
        }

        public void AddPostComment(PostComment newComment)
        {
            _comments.Add(newComment);
        }

        public void RemovePostComment(PostComment toRemove)
        {
            _comments.Remove(toRemove);
        }

        public void UpdatePostComment(Guid postCommentId, string updatedComment)
        {
            var comment = _comments.FirstOrDefault(x => x.PostCommentId == postCommentId);    
            if(comment != null)
                comment.UpdatePostComment(updatedComment);
        }
    }
}
