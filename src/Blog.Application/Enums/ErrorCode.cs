namespace Blog.Application.Enums
{
    public enum ErrorCode
    {
        NotFound = 404,
        UnknownError = 500,

        //Validation Errors
        ValidationErrors = 100,

        //Infrastructure Errors

        //Application Errors
        IdentityUserAlreadyExists = 300,
        IdentityUserDoesNotExists = 301,
        IncorrectUserName = 302,
        IncorrectPassword = 303,
        LockoutOnFailure = 304,
        UnathorizedAccountRemoval = 305,
        CategoryRemovalNotAuthorized = 306,
        CategoryAlreadyExists = 307,
        CategoryHasPosts = 308,
        PostTitleAlreadyExists = 309
    }
}
