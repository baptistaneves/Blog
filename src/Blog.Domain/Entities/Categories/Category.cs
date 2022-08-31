using Blog.Domain.Aggregates.PostAggregate;
using Blog.Domain.Exceptions.Categories;
using Blog.Domain.Validatores.Categories;

namespace Blog.Domain.Entities.Categories
{
    public class Category
    {
        public Guid CategoryId { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastModified { get; private set; }

        //EF - Relation
        public IEnumerable<Post> Posts { get; set; }

        private Category() {}

        /// <summary>
        /// Create a new Category
        /// </summary>
        /// <param name="description">Description category</param>
        /// <returns cref="Category"></returns>
        /// <exception cref="CategoryNotValidException"></exception>
        public static Category CreateCategory(string description)
        {
            return CreateValidation(description);
        }

        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="description">Description category</param>
        /// <exception cref="CategoryNotValidException"></exception>
        /// <returns see cref="Category"></returns>
        public void UpdateCategory(string description)
        {
            UpdateValidation(description);

            Description = description;
            LastModified = DateTime.UtcNow;
        }

        private void UpdateValidation(string description)
        {
            var validator = new CategoryValidator();

            var objToValidate = new Category
            {
                Description = description
            };

            var validationResult = validator.Validate(objToValidate);

            if (!validationResult.IsValid)
            {
                var exception = new CategoryNotValidException("Categoria inválida");

                validationResult.Errors.ForEach(error => exception.ValidationErrors.Add(error.ErrorMessage));

                throw exception;
            }
        }

        private static Category CreateValidation(string description)
        {
            var validator = new CategoryValidator();

            var objToValidate = new Category
            {
                Description = description,
                CreatedAt = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };

            var validationResult = validator.Validate(objToValidate);

            if (!validationResult.IsValid)
            {
                var exception = new CategoryNotValidException("Categoria inválida");

                validationResult.Errors.ForEach(error => exception.ValidationErrors.Add(error.ErrorMessage));

                throw exception;
            }

            return objToValidate;
        }
    }
}
