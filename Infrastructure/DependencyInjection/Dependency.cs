using Application.Interfaces;
using Application.Interfaces.ForAuth;
using Application.Notifications;
using Core.Interfaces;
using Infrastructure.Notifications;
using Infrastructure.Notifications.Providers;
using Infrastructure.Persistance.Repositories;
using Infrastructure.Persistance.Repositories.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resend;

namespace Infrastructure.DependencyInjection
{
    public static class Dependency
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddResend(options =>
            options.ApiToken = config["Resend:ApiKey"]!);

            services.Configure<EmailProviderOptions>(
                config.GetSection("Notifications:Email"));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<IAdminUserRepository, AdminUserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<INotificationProvider, EmailNotificationProvider>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<ILoanRepaymentRepository, LoanRepaymentRepository>();

            return services;
        }
    }
}
