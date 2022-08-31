namespace Blog.Api.Contracts.UserProfiles.Response
{
    public class UserProfileResponse
    {
        public Guid UserProfileId { get; set; }
        public string IdentityId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
        public string Role { get; set; }
        public BasinInformation BasicInfo { get; set; }

    }
}
