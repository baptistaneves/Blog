using Blog.Application.MappingProfiles;
using Blog.Application.Services;

namespace Blog.Api.Registrars
{
    public class ApplicationLayerRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(IdentityMapping));
            builder.Services.AddScoped<IdentityService>();
        }
    }
}
