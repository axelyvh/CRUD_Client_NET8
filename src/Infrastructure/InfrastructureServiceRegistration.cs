using Application.Managers;
using Application.Repositories.Base;
using Infrastructure.Managers;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ApplicationConnection"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericAsyncRepository<>), typeof(GenericAsyncRepository<>));

            services.AddScoped<IUtilManager, UtilManager>();
            services.AddScoped<IFileManager, FileManager>();

            return services;

        }
    }
}
