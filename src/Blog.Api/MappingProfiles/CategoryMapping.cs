using Blog.Api.Contracts.Categories.Response;
using Blog.Domain.Entities.Categories;

namespace Blog.Api.MappingProfiles
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryResponse>();
        }
    }
}
