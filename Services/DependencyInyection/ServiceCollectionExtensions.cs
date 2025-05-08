using ApiPortafolio.Interfaces.IRepositories;
using ApiPortafolio.Interfaces.IServices;
using ApiPortafolio.Repositories;

namespace ApiPortafolio.Services.DependencyInyection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IContactoEmpresaRepositorio, ContactoEmpresaRepositorio>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<EmpresaContactoService>();

            return services;
        }
    }
}
