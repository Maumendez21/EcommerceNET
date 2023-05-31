using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Models.Email;
using Ecommerce.Application.Models.ImageMangment;
using Ecommerce.Application.Models.Token;
using Ecommerce.Application.Persistence;
using Infraestructure.MessageImplementation;
using Infraestructure.Repositories;
using Infraestructure.Services.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Persistence
{
    public static class InfraestructureServiceRegistration
    {
        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services,
                                                                    IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAuthService, AuthService>();

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            return services;   
        }
    }
}