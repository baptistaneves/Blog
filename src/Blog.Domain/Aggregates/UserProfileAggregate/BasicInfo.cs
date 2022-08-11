using Blog.Domain.Exceptions.UsersProfiles;
using Blog.Domain.Validatores.UsersProfiles;

namespace Blog.Domain.Aggregates.UserProfileAggregate
{
    public class BasicInfo
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public string Phone { get; private set; }

        private BasicInfo()  { }

        /// <summary>
        /// Create BasicInfo instance
        /// </summary>
        /// <param name="firstName">First Name</param>
        /// <param name="lastName">Last Name</param>
        /// <param name="emailAddress">Email Address</param>
        /// <param name="phone">Phone</param>
        /// <returns see cref="BasicInfo"></returns>
        public static BasicInfo CreateBasicInfo(string firstName, string lastName, 
            string emailAddress, string phone)
        {
            var validator = new BasicInfoValidator();

            var objToValidate = new BasicInfo
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                Phone = phone
            };

            var validationResult = validator.Validate(objToValidate);
            if (validationResult.IsValid) return objToValidate;

            var exception = new UserProfileNotValidException("O perfil do usuário não é válido");

            validationResult.Errors.ForEach(error => exception.ValidationErrors.Add(error.ErrorMessage));

            throw exception;
        }
    }
}
