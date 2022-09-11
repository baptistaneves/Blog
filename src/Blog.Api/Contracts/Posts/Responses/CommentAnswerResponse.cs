namespace Blog.Api.Contracts.Posts.Responses
{
    public class CommentAnswerResponse
    {
        public Guid CommentAnswerId { get; set; }
        public Guid PostCommentId { get; set; }
        public Guid UserProfileId { get; set; }
        public string UserFullName { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
