namespace Blog.Api.Contracts.Identity.Response
{
    public class IdentityUserProfileResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Token { get; set; }
    }
}
