namespace Blog.Application.Posts.Dtos
{
    public class PostDto
    {
        public Guid PostId { get; set; }
        public Guid UserProfileId { get; set; }
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string CreatedBy { get; set; }
        public string CategoryDescritpion { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
    }
}
