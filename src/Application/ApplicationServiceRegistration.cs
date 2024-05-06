using Application.UseCases.AttachmentUseCase;
using Application.UseCases.ClientUseCase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IAttachmentUseCase, AttachmentUseCase>();
            services.AddScoped<IClientUseCase, ClientUseCase>();

            return services;

        }
    }
}