using Blog.Domain.Aggregates.PostAggregate;

namespace Blog.Domain.Aggregates.UserProfileAggregate
{
    public class UserProfile
    {
        public Guid UserProfileId { get; private set; }
        public string IdentityId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastModified { get; private set; }
        public string Role { get; private set; }
        public BasicInfo BasicInfo { get; private set; }

        //EF - Relation
        public IEnumerable<Post> Posts { get; private set; }
        public IEnumerable<PostComment> PostComments { get; private set; }
        public IEnumerable<PostReaction> PostReactions { get; private set; }
        public IEnumerable<CommentAnswer> PostCommentResponses { get; private set; }
        public IEnumerable<CommentReaction> PostCommentReactions { get; private set; }

        private UserProfile()  { }

        /// <summary>
        /// Create User Profile
        /// </summary>
        /// <param name="identityId">Identifier of identity user</param>
        /// <param name="basicInfo">Basic information of the user profile</param>
        /// <returns see cref="UserProfile"></returns>
        public static UserProfile CreateUserProfile(string identityId, string role, BasicInfo basicInfo)
        {
            return new UserProfile
            {
                IdentityId = identityId,
                Role = role,
                CreatedAt = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                BasicInfo = basicInfo
            };
        }

        /// <summary>
        /// Update basic information
        /// </summary>
        /// <param name="newInfo">Basic information</param>
        public void UpdateBasicInfo(BasicInfo newInfo)
        {
            BasicInfo = newInfo;
            LastModified = DateTime.UtcNow;
        }
    }
}
