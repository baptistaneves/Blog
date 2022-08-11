namespace Blog.Api.Registrars
{
    public interface IWebApplicationRegistrar : IRegistrar
    {
        void RegisterServices(WebApplication app);
    }
}
