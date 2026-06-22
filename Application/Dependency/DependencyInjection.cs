using Application.Features.Users.Commands.Register;
using Application.Interfaces;
using Application.Interfaces.ForAuth;
using Application.Mapping;
using Application.Services;
using Application.Validations.User;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Microsoft.AspNet.Identity;
using Core.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Core.Entities.Admins;

namespace Application.Dependency
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly));

            services.AddValidatorsFromAssemblyContaining<UserRequestValidator>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IPasswordHasher<AdminUsers>, PasswordHasher<AdminUsers>>();

            return services;
        }
    }
}
