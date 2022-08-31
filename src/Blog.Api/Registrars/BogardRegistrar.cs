using Blog.Application.Identity.Commands;
using System.Reflection;

namespace Blog.Api.Registrars
{
    public class BogardRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddMediatR(typeof(RegisterIdentityCommand));
        }
    }
}
